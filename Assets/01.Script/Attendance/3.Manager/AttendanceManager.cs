using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager instance;

    [SerializeField]
    private int _dayCheat;

    [SerializeField]
    private List<AttendanceSO> _metaDatas;

    private List<Attendance> _attendanceList;

    private AttendanceRepository _attendanceRepository;

    public event Action OnAttendanceChanged;

    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 초기화
        _attendanceList = new List<Attendance>(_metaDatas.Count);

        _attendanceRepository = new AttendanceRepository();

        List<AttendanceSaveData> saveDatas = _attendanceRepository.Load(AccountManager.instance.CurrentAccount.Email);

        foreach (var meta in _metaDatas)
        {
            var saveData = saveDatas?.Find(x => x.AttendanceChannel == meta.AttendanceChannel);

            var attendance = new Attendance(meta, saveData, DateTime.Now.AddDays(_dayCheat));

            _attendanceList.Add(attendance);
        }
        
        OnAttendanceChanged?.Invoke();
    }

    public bool TryReceiveReward(AttendanceRewardDTO reward)
    {
        Attendance attendance = _attendanceList.Find(x => x.AttendanceChannel == reward.AttendanceChannel);
        if (attendance.TryReceiveReward(DateTime.Now.AddDays(_dayCheat), reward.Day))
        {
            // 보상 수령
            CurrencyManager.Instance.Add(reward.RewardCurrencyType, reward.RewardAmount);
            Debug.Log($"Received reward: {reward.RewardCurrencyType}, {reward.RewardAmount}");

            OnAttendanceChanged?.Invoke();
            
            _attendanceRepository.Save(_attendanceList.ConvertAll(x => x.ToDTO()), AccountManager.instance.CurrentAccount.Email);

            return true;
        }
        else
        {
            // 보상 수령 실패
            return false;
        }
    }

    public AttendanceDTO GetAttendanceDto(EAttendanceChannel channel)
    {
        return _attendanceList.Find(x => x.AttendanceChannel == channel).ToDTO();
    }

    public bool IsAttendanceActive(EAttendanceChannel channel)
    {
        return _attendanceList.Find(x => x.AttendanceChannel == channel).StartDate < DateTime.Now.AddDays(_dayCheat);
    }

    public int GetRemainingDays(EAttendanceChannel channel)
    {
        return (_attendanceList.Find(x => x.AttendanceChannel == channel).DeadlineDate - DateTime.Now.AddDays(_dayCheat)).Days;
    }
}