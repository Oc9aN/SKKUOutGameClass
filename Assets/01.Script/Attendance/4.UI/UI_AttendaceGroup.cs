using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendaceGroup : MonoBehaviour
{
    public List<UI_Attendance> AttendanceGroups;
    public List<Button> AttendanceButtons;

    private void Start()
    {
        for (int i = 0; i < AttendanceButtons.Count; i++)
        {
            int index = i; // 클로저 문제 해결을 위해 로컬 변수 사용
            AttendanceButtons[index].onClick.AddListener(() => OnButtonClick(index));
        }

        for (int i = 0; i < AttendanceGroups.Count; i++)
        {
            bool isActive = AttendanceManager.instance.IsAttendanceActive(AttendanceGroups[i].AttendanceChannel);
            AttendanceGroups[i].gameObject.SetActive(isActive);
            AttendanceButtons[i].gameObject.SetActive(isActive);
        }

        OnButtonClick(0);
    }
    
    private void OnButtonClick(int buttonIndex)
    {
        // 모든 게임 오브젝트 비활성화
        DeactivateAllGameObjects();

        // 선택된 인덱스의 게임 오브젝트만 활성화
        if (buttonIndex >= 0 && buttonIndex < AttendanceGroups.Count && AttendanceGroups[buttonIndex] != null)
        {
            AttendanceGroups[buttonIndex].gameObject.SetActive(true);
        }
    }

    private void DeactivateAllGameObjects()
    {
        foreach (var uiAttendance in AttendanceGroups)
        {
            if (uiAttendance != null)
            {
                uiAttendance.gameObject.SetActive(false);
            }
        }
    }
}
