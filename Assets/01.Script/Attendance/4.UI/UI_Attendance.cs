using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    [SerializeField]
    private EAttendanceChannel _attendanceChannel;

    public EAttendanceChannel AttendanceChannel => _attendanceChannel;

    [SerializeField]
    private List<UI_AttendanceReward> _rewards;

    [SerializeField]
    private TextMeshProUGUI _remainDayTextUI;

    [SerializeField]
    private TextMeshProUGUI _consecutiveCountTextUI;

    private void Start()
    {
        AttendanceManager.instance.OnAttendanceChanged += Refresh;

        foreach (UI_AttendanceReward reward in _rewards)
        {
            reward.OnAttendanceChanged += Refresh;
        }

        Refresh();
    }

    private void Refresh()
    {
        AttendanceDTO attendanceDto = AttendanceManager.instance.GetAttendanceDto(_attendanceChannel);

        _remainDayTextUI.text = $"{AttendanceManager.instance.GetRemainingDays(_attendanceChannel)}일 남았습니다.";
        _consecutiveCountTextUI.text = _attendanceChannel == EAttendanceChannel.Normal
            ? $"{attendanceDto.ConsecutiveCount}일 연속 출석중입니다."
            : $"{attendanceDto.Rewards.Count}일 출석 시 최종보상을 받을수 있습니다.";

        for (int i = 0; i < attendanceDto.Rewards.Count; i++)
        {
            _rewards[i].Refresh(attendanceDto.Rewards[i]);
        }
    }
}