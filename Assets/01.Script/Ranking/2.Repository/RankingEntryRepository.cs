using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class RankingEntryRepository
{
    private const string RankingKey = "Ranking_All";

    // ��� �÷��̾��� ��ŷ ������ �ϳ��� ����Ʈ�� ����
    public void SaveRanking(RankingEntry entry)
    {
        var allList = LoadAllRankingList();

        // ���� �÷��̾� ��� ã��
        var existing = allList.FirstOrDefault(e => e.PlayerNickname == entry.PlayerNickname);

        bool updated = false;

        if (existing != null)
        {
            // KillPoint(Score) ����
            if (entry.Score > existing.Score)
            {
                existing.Score = entry.Score;
                existing.AchievedAt = entry.AchievedAt;
                updated = true;
            }

            // SurvivalTime ����
            if (entry.SurvivalTime > existing.SurvivalTimeSeconds)
            {
                existing.SurvivalTimeSeconds = entry.SurvivalTime;
                existing.AchievedAt = entry.AchievedAt;
                updated = true;
            }

            // �� �� �� ���� ������ �������� ����
            if (!updated)
                return;
        }
        else
        {
            allList.Add(new RankingEntryDTO(entry));
        }

        // ����
        string json = JsonUtility.ToJson(new RankingDTOListWrapper { List = allList });
        PlayerPrefs.SetString(RankingKey, json);
        PlayerPrefs.Save();
    }


    // ��ŷ Ÿ�Ժ��� �и��ؼ� ��ȯ
    public List<RankingEntryDTO> LoadRankingList(RankingType rankingType)
    {
        var allList = LoadAllRankingList();

        if (rankingType == RankingType.KillPoint)
            return allList.Where(e => e.Score >= 0)
                          .OrderByDescending(e => e.Score)
                          .ToList();
        else 
            return allList.Where(e => e.SurvivalTimeSeconds >= 0)
                          .OrderByDescending(e => e.SurvivalTimeSeconds)
                          .ToList();
    }

    // ��ü ��ŷ ����Ʈ �ε� (���ο�)
    private List<RankingEntryDTO> LoadAllRankingList()
    {
        if (!PlayerPrefs.HasKey(RankingKey))
            return new List<RankingEntryDTO>();

        string json = PlayerPrefs.GetString(RankingKey);
        var wrapper = JsonUtility.FromJson<RankingDTOListWrapper>(json);
        return wrapper?.List ?? new List<RankingEntryDTO>();
    }

    public void AddTestRankingData(RankingType rankingType)
    {
        var tempEntries = new List<RankingEntry>
        {
            new RankingEntry("TestUser1", 2000, 0f, DateTime.Now),
            new RankingEntry("TestUser2", 1900, 1f, DateTime.Now),
            new RankingEntry("TestUser3", 1800, 0f, DateTime.Now),
            new RankingEntry("TestUser4", 1700, 3f, DateTime.Now),
            new RankingEntry("TestUser5", 1600, 0f, DateTime.Now),
            new RankingEntry("TestUser6", 1500, 4f, DateTime.Now),
            new RankingEntry("TestUser7", 1400, 0f, DateTime.Now),
            new RankingEntry("TestUser8", 1300, 0f, DateTime.Now),
            new RankingEntry("TestUser9", 1200, 5f, DateTime.Now),
            new RankingEntry("TestUser10", 1100, 2f, DateTime.Now),
            new RankingEntry("TestUser11", 0, 600f, DateTime.Now),
            new RankingEntry("TestUser12", 1, 580f, DateTime.Now),
            new RankingEntry("TestUser13", 0, 560f, DateTime.Now),
            new RankingEntry("TestUser14", 0, 540f, DateTime.Now),
            new RankingEntry("TestUser15", 0, 520f, DateTime.Now),
            new RankingEntry("TestUser16", 0, 500f, DateTime.Now),
            new RankingEntry("TestUser17", 0, 480f, DateTime.Now),
            new RankingEntry("TestUser18", 0, 460f, DateTime.Now),
            new RankingEntry("TestUser19", 0, 440f, DateTime.Now),
            new RankingEntry("TestUser20", 0, 420f, DateTime.Now)
        };

        foreach (var entry in tempEntries)
        {
            // KillPoint ��ŷ���� Score > 0�� �����͸�, SurvivalTime ��ŷ���� SurvivalTime > 0�� �����͸� �߰�
            SaveRanking(entry);
            // if (rankingType == RankingType.KillPoint && entry.Score > 0)
            //     SaveRanking(entry);
            // else if (rankingType == RankingType.SurvivalTime && entry.SurvivalTime > 0)
            //     SaveRanking(entry);
        }
    }
}

[Serializable]
public class RankingDTOListWrapper
{
    public List<RankingEntryDTO> List = new();
}