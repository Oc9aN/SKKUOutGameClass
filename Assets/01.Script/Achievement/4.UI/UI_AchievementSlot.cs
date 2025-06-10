using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDateTextUI;
    public Button RewardClaimButton;
    public int CurrentValue;
    
    private AchievementDTO _achievementDto;

    public void Refresh(AchievementDTO achievementDto)
    {
        _achievementDto = achievementDto;
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