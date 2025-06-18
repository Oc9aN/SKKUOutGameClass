using System;
using System.Collections.Generic;

public class RankingManager : MonoBehaviourSingleton<RankingManager>
{
    private RankingRepository _repository;
    private List<Ranking> _rankings;
    
    private Ranking _myRanking;

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
            Ranking ranking = new Ranking(saveData.Email, saveData.NickName, saveData.Score);
            _rankings.Add(ranking);

            if (ranking.Email == AccountManager.instance.CurrentAccount.Email)
            {
                _myRanking = ranking;
            }
        }

        if (_myRanking == null)
        {
            AccountDto me = AccountManager.instance.CurrentAccount;
            _myRanking = new Ranking(me.Email, me.Nickname, 0);
            
            _rankings.Add(_myRanking);
        }
        
        OnDataChanged?.Invoke();
    }

    private void Sort()
    {
        _rankings.Sort((r1, r2) => r1.Score.CompareTo(r2.Score));

        for (int i = 0; i < _rankings.Count; i++)
        {
            _rankings[i].SetRank(i + 1);
        }
    }
}