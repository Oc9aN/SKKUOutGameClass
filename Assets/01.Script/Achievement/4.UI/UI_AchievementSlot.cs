using Gpm.Ui;
using TMPro;
using UnityEngine.UI;

public class UI_AchievementSlotData : InfiniteScrollData
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;

    public int CurrentValue;
    public bool RewardClaimed;

    public UI_AchievementSlotData(AchievementDTO dto)
    {
        ID = dto.ID;
        Name = dto.Name;
        Description = dto.Description;
        Condition = dto.Condition;
        GoalValue = dto.GoalValue;
        RewardCurrencyType = dto.RewardCurrencyType;
        RewardAmount = dto.RewardAmount;
        CurrentValue = dto.CurrentValue;
        RewardClaimed = dto.RewardClaimed;
    }
}

public class UI_AchievementSlot : InfiniteScrollItem
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDateTextUI;
    public Button RewardClaimButton;
    
    private AchievementDTO _achievementDto;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);
        
        UI_AchievementSlotData achievementSlotData = scrollData as UI_AchievementSlotData;
        
        _achievementDto = new AchievementDTO(
            achievementSlotData.ID,
            achievementSlotData.Name,
            achievementSlotData.Description,
            achievementSlotData.Condition,
            achievementSlotData.GoalValue,
            achievementSlotData.RewardCurrencyType,
            achievementSlotData.RewardAmount,
            achievementSlotData.CurrentValue,
            achievementSlotData.RewardClaimed
        );
        
        NameTextUI.text = _achievementDto.Name;
        DescriptionTextUI.text = _achievementDto.Description;
        RewardCountTextUI.text = _achievementDto.RewardAmount.ToString();
        ProgressSlider.value = (float)_achievementDto.CurrentValue / _achievementDto.GoalValue;
        ProgressTextUI.text = $"{_achievementDto.CurrentValue} / {_achievementDto.GoalValue}";

        RewardClaimButton.interactable = _achievementDto.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.instance.TryClaimReward(_achievementDto))
        {
            // 달성 효과
        }
        else
        {
            // 진행도 부족 팝업
        }
    }
}