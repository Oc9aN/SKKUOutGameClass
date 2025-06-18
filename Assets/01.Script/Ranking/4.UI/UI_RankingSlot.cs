using System;
using Gpm.Ui;
using TMPro;
using UnityEngine;

public class RankingSlotData : InfiniteScrollData
{
    public int Rank;
    public string Nickname;
    public float Time;
    public int Score;

    public RankingSlotData(RankingDTO data)
    {
        Rank = data.Rank;
        Nickname = data.Nickname;
        Time = data.Time;
        Score = data.Score;
    }

    public void SetRankingDTOData(RankingDTO data)
    {
        Rank = data.Rank;
        Nickname = data.Nickname;
        Time = data.Time;
        Score = data.Score;
    }
}

public class UI_RankingSlot : InfiniteScrollItem
{
    public TextMeshProUGUI RankingText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);
        
        // 데이터 변환하여 등록
        var rankingSlotData = scrollData as RankingSlotData;
        
        RankingText.text = rankingSlotData?.Rank.ToString() ?? "-";
        NameText.text = rankingSlotData?.Nickname ?? "-";
        TimeText.text = FloatToFormattedTime(rankingSlotData?.Time ?? 0f);
        ScoreText.text = rankingSlotData?.Score.ToString() ?? "-";
    }
    
    string FloatToFormattedTime(float timeInSeconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(timeInSeconds);

        // 시간, 분, 초를 두 자리 숫자로 포맷팅합니다.
        // d2는 숫자를 두 자리로 채우고, 필요하면 앞에 0을 붙입니다.
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds);
    }
}