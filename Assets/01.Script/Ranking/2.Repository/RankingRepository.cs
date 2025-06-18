using System;
using System.Collections.Generic;
using UnityEngine;

public class RankingRepository
{
    private const string SAVE_KEY = nameof(RankingRepository);
    
    // public void Save(List<RankingDTO> dataList)
    // {
    //     RankingSaveDataList datas = new RankingSaveDataList();
    //     datas.list = dataList.ConvertAll(x => new RankingSaveData(x));
    //
    //     string json = JsonUtility.ToJson(datas);
    //     PlayerPrefs.SetString(SAVE_KEY, json);
    // }
    
    public List<RankingSaveData> Load()
    {
        // 데이터가 아직 없지만 ..
        // 개발 단계에서 데이터가 필요하다면.. Mocking 기법을 쓴다.
        // PlayerPrefs대신 '가짜 데이터 반환'
        var mock = new List<RankingSaveData>()
        {
new RankingSaveData(1813, "test1@test.com", "냉철한토끼65", 145.3f),
            new RankingSaveData(2721, "test2@test.com", "빛나는호랑이922", 198.7f),
            new RankingSaveData(2960, "test3@test.com", "달콤한햄스터489", 101.2f),
            new RankingSaveData(2263, "test4@test.com", "따뜻한토끼754", 212.5f),
            new RankingSaveData(1552, "test5@test.com", "귀여운여우451", 88.4f),
            new RankingSaveData(2086, "test6@test.com", "행복한고양이621", 174.1f),
            new RankingSaveData(2065, "test7@test.com", "따뜻한햄스터558", 156.9f),
            new RankingSaveData(1211, "test8@test.com", "우주고양이980", 236.0f),
            new RankingSaveData(2139, "test9@test.com", "무서운사자570", 191.2f),
            new RankingSaveData(1469, "test10@test.com", "우주토끼732", 112.8f),
            new RankingSaveData(2786, "test11@test.com", "행복한여우85", 252.3f),
            new RankingSaveData(2850, "test12@test.com", "귀여운토끼586", 75.9f),
            new RankingSaveData(3100, "test13@test.com", "냉철한늑대739", 267.1f),
            new RankingSaveData(3034, "test14@test.com", "냉철한여우560", 134.0f),
            new RankingSaveData(2446, "test15@test.com", "빛나는고양이474", 118.2f),
            new RankingSaveData(3175, "test16@test.com", "따뜻한여우884", 259.8f),
            new RankingSaveData(1056, "test17@test.com", "귀여운곰674", 95.4f),
            new RankingSaveData(2492, "test18@test.com", "행복한햄스터538", 123.1f),
            new RankingSaveData(1527, "test19@test.com", "미친여우342", 174.3f),
            new RankingSaveData(2710, "test20@test.com", "달콤한토끼618", 220.5f),
            new RankingSaveData(2869, "test21@test.com", "행복한호랑이271", 147.6f),
            new RankingSaveData(2825, "test22@test.com", "행복한곰35", 185.2f),
            new RankingSaveData(1045, "test23@test.com", "고요한고양이417", 99.9f),
            new RankingSaveData(2503, "test24@test.com", "행복한고양이796", 214.6f),
            new RankingSaveData(2992, "test25@test.com", "무서운곰112", 164.8f),
            new RankingSaveData(2265, "test26@test.com", "달콤한고양이669", 119.3f),
            new RankingSaveData(2060, "test27@test.com", "우주햄스터18", 173.5f),
            new RankingSaveData(1108, "test28@test.com", "따뜻한사자117", 190.0f),
            new RankingSaveData(1762, "test29@test.com", "미친판다565", 152.7f),
            new RankingSaveData(1589, "test30@test.com", "우주고양이491", 208.9f),
            new RankingSaveData(2546, "test31@test.com", "귀여운늑대579", 130.2f),
            new RankingSaveData(2895, "test32@test.com", "행복한여우38", 188.6f),
            new RankingSaveData(3134, "test33@test.com", "미친햄스터637", 175.0f),
            new RankingSaveData(1165, "test34@test.com", "미친햄스터526", 210.1f),
            new RankingSaveData(3341, "test35@test.com", "따뜻한사자758", 270.9f),
            new RankingSaveData(3130, "test36@test.com", "무서운늑대159", 84.7f),
            new RankingSaveData(2464, "test37@test.com", "냉철한호랑이995", 153.6f),
            new RankingSaveData(1229, "test38@test.com", "달콤한사자524", 91.3f),
            new RankingSaveData(1641, "test39@test.com", "귀여운판다495", 232.4f),
            new RankingSaveData(2241, "test40@test.com", "고요한여우971", 204.5f),
            new RankingSaveData(1936, "test41@test.com", "우주곰490", 163.8f),
            new RankingSaveData(1521, "test42@test.com", "무서운여우785", 72.1f),
            new RankingSaveData(1584, "test43@test.com", "따뜻한늑대857", 194.4f),
            new RankingSaveData(1996, "test44@test.com", "귀여운곰189", 142.0f),
            new RankingSaveData(2161, "test45@test.com", "우주독수리75", 160.7f),
            new RankingSaveData(1729, "test46@test.com", "귀여운곰394", 214.2f),
            new RankingSaveData(2398, "test47@test.com", "미친호랑이739", 250.6f),
            new RankingSaveData(2360, "test48@test.com", "따뜻한곰721", 177.3f),
            new RankingSaveData(1865, "test49@test.com", "빛나는햄스터343", 103.5f),
            new RankingSaveData(3020, "test50@test.com", "냉철한호랑이494", 197.0f),
            new RankingSaveData(1697, "test100@test.com", "행복한토끼588", 165.9f),
        };
        return mock;
    }
}

[Serializable]
public class RankingSaveData
{
    public int Score;
    public string Email;
    public string NickName;
    public float Time;

    public RankingSaveData(RankingDTO data) : this(data.Rank, data.Email, data.Nickname, data.Time)
    {
        
    }

    public RankingSaveData(int score, string email, string nickName, float time)
    {
        Score = score;
        Email = email;
        NickName = nickName;
        Time = time;
    }
}

[Serializable]
public class RankingSaveDataList
{
    public List<RankingSaveData> list;
}