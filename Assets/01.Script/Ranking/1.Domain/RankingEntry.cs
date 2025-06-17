using System;

public enum RankingType
{
    KillPoint,   // 적 처치 포인트 랭킹
    SurvivalTime // 생존 시간 랭킹
}

public class RankingEntry
{
    public int PlayerID { get; set; } // 플레이어 ID (추가 정보로 사용 가능)
    public string PlayerNickname { get; }
    public int Score { get; private set; } // KillPoint용
    public float SurvivalTime { get; private set; }
    public DateTime AchievedAt { get; } // 랭킹 달성 시간

    public RankingEntry()
    {
    }

    public RankingEntry(string playerNickname, int score, float survivalTime, DateTime achievedAt)
    {
        if (string.IsNullOrWhiteSpace(playerNickname))
            throw new ArgumentException("플레이어 닉네임은 비어 있을 수 없습니다.", nameof(playerNickname));
        if (score < 0)
            throw new ArgumentOutOfRangeException(nameof(score), "점수는 0 이상이어야 합니다.");
        if (survivalTime < 0)
            throw new ArgumentOutOfRangeException(nameof(survivalTime), "생존 시간은 0 이상이어야 합니다.");

        PlayerNickname = playerNickname;
        Score = score;
        SurvivalTime = survivalTime;
        AchievedAt = achievedAt;
    }

    public void AddScore(int additionalScore)
    {
        if (additionalScore < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalScore), "추가 점수는 0 이상이어야 합니다.");

        Score += additionalScore;
    }

    public void SetSurvivalTime(float survivalTime)
    {
        if (survivalTime < 0)
            throw new ArgumentOutOfRangeException(nameof(survivalTime), "생존 시간은 0 이상이어야 합니다.");

        SurvivalTime = survivalTime;
    }
}