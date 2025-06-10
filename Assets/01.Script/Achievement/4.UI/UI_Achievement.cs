using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _slots;

    private void Start()
    {
        Refresh();
        
        AchievementManager.instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}