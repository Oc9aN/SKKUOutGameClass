using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRepository
{
    private const string SAVE_KEY = nameof(CollectibleRepository);

    public void Save(CollectibleProgress progress)
    {
        var saveData = new CollectibleProgressSaveData
        {
            UserId = progress.UserId,
            CollectedIds = new List<string>()
        };

        foreach (var collectible in progress.GetAll())
        {
            if (collectible.IsCollected)
            {
                saveData.CollectedIds.Add(collectible.Id);
            }
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY + "_" + progress.UserId, json);
        PlayerPrefs.Save();
    }

    public CollectibleProgress Load(string userId = "default")
    {
        string key = SAVE_KEY + "_" + userId;

        if (!PlayerPrefs.HasKey(key))
        {
            return new CollectibleProgress(userId); // 비어 있는 상태로 초기화
        }

        string json = PlayerPrefs.GetString(key);
        var saveData = JsonUtility.FromJson<CollectibleProgressSaveData>(json);

        var progress = new CollectibleProgress(saveData.UserId);
        foreach (string id in saveData.CollectedIds)
        {
            var collectible = new Collectible(id);
            collectible.Collect(DateTime.UtcNow); // 저장된 시간 정보가 없기 때문에 임의로 처리
            progress.Register(collectible);
        }

        return progress;
    }
}

[Serializable]
public class CollectibleProgressSaveData
{
    public string UserId;
    public List<string> CollectedIds;
}