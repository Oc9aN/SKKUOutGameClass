using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private UI_AchievementSlot _slotPrefab;
    [SerializeField]
    private Transform _slotParent;
    
    private List<UI_AchievementSlot> _slots;

    private void Start()
    {
        _slots = new List<UI_AchievementSlot>();
        
        Refresh();
        
        AchievementManager.instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            if (_slots.Count <= i)
            {
                UI_AchievementSlot newSlot = Instantiate(_slotPrefab, _slotParent);
                _slots.Add(newSlot);
            }
            _slots[i].Refresh(achievements[i]);
        }
    }
}