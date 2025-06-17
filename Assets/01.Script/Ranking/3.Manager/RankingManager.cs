using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance;

    private RankingRepository _repository;
    private RankingEntry _rankingEntry;
    private float _sessionStartTime;
    
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        // 저장소 초기화
        _repository = new RankingRepository();  // :contentReference[oaicite:0]{index=0}
        _repository.AddTestRankingData(RankingType.KillPoint);
        _rankingEntry = new RankingEntry();
    }

    /// <summary>
    /// 게임 세션 타이머 시작.
    /// 게임 시작 시점에 호출하세요.
    /// </summary>
    public void StartTimer()
    {
        _sessionStartTime = Time.realtimeSinceStartup;
    }
    
    public List<RankingEntryDTO> GetTopRankings(RankingType rankingType)
    {
        List<RankingEntryDTO> entries = _repository.LoadRankingList(rankingType);
        return entries;
    }
    
    /// <summary>
    /// 세션 타이머를 멈추고, 경과 시간을 초 단위로 반환합니다.
    /// 게임 오버 또는 클리어 시점에 호출하세요.
    /// </summary>
    /// <returns>경과 시간(초)</returns>
    public float StopTimer()
    {
        float elapsed = Time.realtimeSinceStartup - _sessionStartTime;
        return elapsed;
    }
    
    public void AddScore(int additionalScore)
    {
        _rankingEntry.AddScore(additionalScore);
    }
    
    /// <summary>
    /// 게임 오버 시 또는 새로고침 버튼 클릭 시 호출합니다.
    /// 현재 계정의 점수와 생존 시간을 저장하고,
    /// 지정된 타입의 랭킹을 다시 로드합니다.
    /// </summary>
    public void Save()
    {
        // 현재 로그인된 계정의 닉네임 조회
        string nickname = AccountManager.instance.CurrentAccount.Nickname;  // :contentReference[oaicite:1]{index=1}
        float time = StopTimer();

        // 도메인 엔트리 생성 (유효성 검사 포함)
        _rankingEntry = new RankingEntry(
            nickname,
            _rankingEntry.Score,
            time,
            DateTime.Now
        );  // :contentReference[oaicite:2]{index=2}

        // 저장소에 저장
        _repository.SaveRanking(_rankingEntry);  // :contentReference[oaicite:3]{index=3}
        
        _rankingEntry = new RankingEntry();
    }

    /// <summary>
    /// 현재 계정의 랭킹 정보를 DTO 형태로 반환합니다.
    /// </summary>
    public RankingEntryDTO Get()
    {
        return new RankingEntryDTO(_rankingEntry);
    }
}