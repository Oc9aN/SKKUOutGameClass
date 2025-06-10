using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private List<Achievement> _achievements;
    
    // 외부에서 사용할 때는 항상 DTO로 안전하게 접근한다.
    public List<AchievementDTO> Achievements => _achievements.ConvertAll(x => new AchievementDTO(x));

    public event Action OnDataChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 초기화
        
        _achievements = new List<Achievement>();
        
        foreach (var meta in _metaDatas)
        {
            _achievements.Add(new Achievement(meta));
        }
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                achievement.Increase(value);
            }
        }
        
        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = _achievements.Find(a => a.ID == achievementDto.ID);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            
            OnDataChanged?.Invoke();
            
            return true;
        }

        return false;
    }
}
