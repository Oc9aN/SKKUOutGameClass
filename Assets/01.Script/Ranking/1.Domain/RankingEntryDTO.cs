using System;

[Serializable]
public class RankingEntryDTO
{
    public string PlayerNickname;
    public int Score;
    public float SurvivalTimeSeconds;
    public DateTime AchievedAt;

    public RankingEntryDTO() { }

    public RankingEntryDTO(RankingEntry entry)
    {
        PlayerNickname = entry.PlayerNickname;
        Score = entry.Score;
        SurvivalTimeSeconds = entry.SurvivalTime;
        AchievedAt = entry.AchievedAt;
    }

    public RankingEntry ToDomain()
    {
        return new RankingEntry(
            PlayerNickname,
            Score,
            SurvivalTimeSeconds,
            AchievedAt
        );
    }
}
