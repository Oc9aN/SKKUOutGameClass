using System.Collections.Generic;

public class LevelDTO
{
    public int CurrentLevel { get; set; }
    public int MaxLevel { get; set; }
    public float MonsterAttackIncrease { get; set; }
    public float MonsterHealthIncrease { get; set; }
    public float SpawnIntervalDecrease { get; set; }
    public int MaxSpawnCount { get; set; }
    public float LevelDuration { get; set; }
    public Dictionary<MonsterType, float> SpawnRate { get; set; }

    public LevelDTO() { }

    public LevelDTO(Level level)
    {
        CurrentLevel = level.CurrentLevel;
        MaxLevel = level.MaxLevel;
        MonsterAttackIncrease = level.MonsterAttackIncrease;
        MonsterHealthIncrease = level.MonsterHealthIncrease;
        SpawnIntervalDecrease = level.SpawnIntervalDecrease;
        MaxSpawnCount = level.MaxSpawnCount;
        LevelDuration = level.LevelDuration;
        SpawnRate = new Dictionary<MonsterType, float>(level.SpawnRate);
    }

    public Level ToDomain(LevelSO levelSO)
    {
        return new Level(
            CurrentLevel,
            MaxLevel,
            MonsterAttackIncrease,
            MonsterHealthIncrease,
            SpawnIntervalDecrease,
            MaxSpawnCount,
            LevelDuration,
            new Dictionary<MonsterType, float>(SpawnRate),
            levelSO
        );
    }
}
