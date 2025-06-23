using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }

    private CollectibleProgress _progress;
    private CollectibleRepository _repository;

    [SerializeField]
    private List<string> allCollectibleIds; // 에디터에서 등록

    private void Awake()
    {
        // 싱글톤 생성
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 의존성 초기화 (간단한 예시)
        _repository = new CollectibleRepository(); // 구현체 필요
        _progress = _repository.Load();
        
        RegisterAllCollectibles();
    }
    
    private void RegisterAllCollectibles()
    {
        foreach (var id in allCollectibleIds)
        {
            if (!_progress.IsCollected(id))
            {
                _progress.Register(new Collectible(id));
            }
        }
    }

    public bool TryCollect(string id)
    {
        if (!_progress.TryCollect(id, DateTime.UtcNow))
            return false;

        _repository.Save(_progress);
        return true;
    }

    public bool IsCollected(string id) => _progress.IsCollected(id);
}