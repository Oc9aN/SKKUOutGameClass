using System;
using System.Collections.Generic;
using System.Linq;
using Gpm.Ui;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private InfiniteScroll _scroll;

    // 구조상 UI데이터 저장 및 DTO를 변환 필요
    private List<UI_AchievementSlotData> _achievementData;

    private void Start()
    {
        _achievementData = new List<UI_AchievementSlotData>();
        Refresh();
        
        AchievementManager.instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            if (_achievementData.Count <= i)
            {
                _achievementData.Add(new UI_AchievementSlotData(achievements[i]));
                _scroll.InsertData(_achievementData.Last());
            }

            // 인게임 중 바뀌는 값만 수정해서 전달
            _achievementData[i].CurrentValue = achievements[i].CurrentValue;
            _achievementData[i].RewardClaimed = achievements[i].RewardClaimed;
            _scroll.UpdateData(_achievementData[i]);
        }
    }
}