using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;
    private const float REWPAWN_TIME = 5f;
    private const int MAX_COUNT = 10;


    private void Update()
    {
        _currentTime += Time.deltaTime;

        var levelInfo = LevelManager.Instance.Level;
        if (_currentTime >= REWPAWN_TIME + levelInfo.SpawnIntervalDecrease)
        {
            _currentTime = 0f;
            
            Debug.Log($"levelInfo: {levelInfo.CurrentLevel}");

            int enemyCount = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
            if (enemyCount >= levelInfo.MaxSpawnCount)
            {
                return;
            }
            
            var randomIndex = Random.Range(0, SpawnPoints.Count);
            Instantiate(EnemyPrefab, SpawnPoints[randomIndex].position, Quaternion.identity);
        }
    }

}