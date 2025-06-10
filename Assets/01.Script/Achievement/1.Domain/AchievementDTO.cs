public class AchievementDTO
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;

    public readonly int CurrentValue;
    public readonly bool RewardClaimed;

    // 메타 정보만 저장하는 생성자
    public AchievementDTO(
        string id, int currentValue, bool rewardClaimed)
    {
        ID = id;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }

    // Achievement 객체에서 전체 상태 복사
    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
    }

    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }
}