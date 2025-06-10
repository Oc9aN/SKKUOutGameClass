using System;

public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger,
}

public class Achievement
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;

    public Achievement(AchievementSO metaData)
    {
        if (string.IsNullOrEmpty(metaData.ID))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Description))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }

        if (metaData.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }

        if (metaData.RewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }

        ID = metaData.ID;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;
    }

    public void Increase(int value)
    {
        if (value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }

        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return _rewardClaimed == false && _currentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if (!CanClaimReward())
        {
            return false;
        }
        _rewardClaimed = true;
        return true;
    }
}