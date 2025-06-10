using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private List<Achievement> _achievements;

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
}
