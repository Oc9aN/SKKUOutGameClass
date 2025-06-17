using System;
using System.Collections.Generic;
using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;
    private const float REWPAWN_TIME = 5f;
    private const int MAX_COUNT = 10;

    private StageLevel _stageLevel;

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        _stageLevel = StageManager.Instance.Stage.CurrentLevel;
    }

    private void Update()
    {
        if (_stageLevel == null)
        {
            return;
        }
        
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= _stageLevel.SpawnInterval)
        {
            _currentTime = 0f;

            foreach (var spawnPoint in SpawnPoints)
            {
                if (Random.Range(0, 100) <= _stageLevel.SpawnRate)
                { 
                    GameObject enemy = Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity);
                    // TODO : 체력 및 공격력 셋팅
                    var health = enemy.GetComponent<Health>();
                }
            }
        }
    }

}