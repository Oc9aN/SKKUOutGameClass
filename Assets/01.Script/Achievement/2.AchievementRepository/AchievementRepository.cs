using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRepository
{
    private const string SAVE_KEY = nameof(AchievementRepository);

    public void Save(List<AchievementDTO> achievements, string email)
    {
        AchievementSaveDataList datas = new AchievementSaveDataList();
        datas.Achievements = achievements.ConvertAll(data => new AchievementSaveData
        {
            ID = data.ID,
            CurrentValue = data.CurrentValue,
            RewardClaimed = data.RewardClaimed,
        });
        
        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + "_" + email, json);
    }

    public List<AchievementSaveData> Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + "_" + email))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + "_" + email);
        AchievementSaveDataList datas = JsonUtility.FromJson<AchievementSaveDataList>(json);

        return datas.Achievements;
    }
}

[Serializable]
public struct AchievementSaveDataList
{
    public List<AchievementSaveData> Achievements;
}
