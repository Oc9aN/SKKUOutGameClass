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
    public event Action<AchievementDTO> OnNewAchievementCanRewarded;
    
    private AchievementRepository _repository;

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
        
        // 저장소
        _repository = new AchievementRepository();
        List<AchievementSaveData> loadedAchievementDatas = _repository.Load();
        foreach (var meta in _metaDatas)
        {
            Achievement duplicateAchievement = FindById(meta.ID);
            if (duplicateAchievement != null)
            {
                throw new Exception($"업적 ID({meta.ID})가 중복됩니다.");
            }
            
            AchievementSaveData saveData = loadedAchievementDatas?.Find(x => x.ID == meta.ID) ?? null;

            // 저장 데이터에 따라 상태 셋팅
            Achievement achievement = new Achievement(meta, saveData);
            
            _achievements.Add(achievement);
        }
    }

    private Achievement FindById(string id)
    {
        return _achievements.Find(x => x.ID == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();
                achievement.Increase(value);
                bool canClaimReward = achievement.CanClaimReward();
                
                _repository.Save(Achievements);

                if (prevCanClaimReward == false && canClaimReward)
                {
                    // 이번 기회에 도전 과정 달성인 경우
                    // 알람
                    OnNewAchievementCanRewarded?.Invoke(new AchievementDTO(achievement));
                }
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
            
            _repository.Save(Achievements);
            
            OnDataChanged?.Invoke();
            
            return true;
        }

        return false;
    }
}
