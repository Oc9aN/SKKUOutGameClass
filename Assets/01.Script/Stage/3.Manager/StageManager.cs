using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public event Action OnDataChanged;

    [SerializeField]
    private List<StageLevelSO> _levelSOList;

    private Stage _stage;
    public Stage Stage => _stage; // TODO: DTO넘기기
    
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
        }

        Init();
    }

    private void Init()
    {
        _stage = new Stage(1, 2, 17f, _levelSOList);
        OnDataChanged?.Invoke();
    }

    private void Update()
    {
        _stage.Progress(Time.deltaTime, OnDataChanged);
    }
}