using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private LevelSO _levelSO;

    private Level _level;
    
    public LevelDTO Level => _level.ToDTO();

    public event Action OnLevelChanged;

    private float _startTime;
    public float StartTime => _startTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Init();
    }

    private void Init()
    {
        _startTime = Time.time;
        _level = new Level(_levelSO);
        StartCoroutine(LevelIncreaseRoutine());
    }

    private IEnumerator LevelIncreaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_level.LevelDuration);
            _level.IncreaseLevel();
            OnLevelChanged?.Invoke();
        }
    }
}