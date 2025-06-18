using System;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviourSingleton<RankingManager>
{
    private RankingRepository _repository;
    private List<Ranking> _rankings;
    
    private Ranking _myRanking;

    private float _startTime;
    
    public List<RankingDTO> Rankings => _rankings.ConvertAll(r => r.ToDTO());
    public RankingDTO MyRanking => _myRanking.ToDTO();

    public event Action OnDataChanged;
    
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        _repository = new RankingRepository();

        List<RankingSaveData> saveDataList = _repository.Load();
        
        _rankings = new List<Ranking>();
        foreach (var saveData in saveDataList)
        {
            Ranking ranking = new Ranking(saveData.Email, saveData.NickName, saveData.Score, saveData.Time);
            _rankings.Add(ranking);

            if (ranking.Email == AccountManager.instance.CurrentAccount.Email)
            {
                _myRanking = ranking;
            }
        }

        if (_myRanking == null)
        {
            AccountDto me = AccountManager.instance.CurrentAccount;
            _myRanking = new Ranking(me.Email, me.Nickname, 0, 0);
            
            _rankings.Add(_myRanking);
        }

        Sort();
        OnDataChanged?.Invoke();
    }

    public void Sort(ERankingSortBy sortBy = ERankingSortBy.Score)
    {
        if (sortBy == ERankingSortBy.Score)
        {
            _rankings.Sort((r1, r2) => r2.Score.CompareTo(r1.Score));
        }
        else
        {
            _rankings.Sort((r1, r2) => r2.Time.CompareTo(r1.Time));
        }

        for (int i = 0; i < _rankings.Count; i++)
        {
            _rankings[i].SetRank(i + 1);
        }
    }
    
    public void AddScore(int score)
    {
        _myRanking.AddScore(score);

        Sort();
        
        OnDataChanged?.Invoke();
    }

    public void StartTimeRecording()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    public void SetTimeRecording()
    {
        float timeRecord = _startTime - Time.realtimeSinceStartup;
        
        _myRanking.SetTime(timeRecord);
    }
}