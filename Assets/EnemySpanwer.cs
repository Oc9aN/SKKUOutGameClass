using System.Collections.Generic;
using Unity.FPS.AI;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    private float _currentTime;
    private const float REWPAWN_TIME = 5f;
    private const int MAX_COUNT = 10;


    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= REWPAWN_TIME)
        {
            _currentTime = 0f;

            int enemyCount = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
            if (enemyCount >= MAX_COUNT)
            {
                return;
            }
            
            var randomIndex = Random.Range(0, SpawnPoints.Count);
            Instantiate(EnemyPrefab, SpawnPoints[randomIndex].position, Quaternion.identity);
        }
    }

}