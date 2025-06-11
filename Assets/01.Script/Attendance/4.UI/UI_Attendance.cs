using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Attendance: MonoBehaviour
{
    [SerializeField]
    private EAttendanceChannel _attendanceChannel;
    [SerializeField]
    private List<UI_AttendanceReward> _rewards;

    private void Start()
    {
        AttendanceManager.instance.OnAttendanceChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        AttendanceDTO attendanceDto = AttendanceManager.instance.GetAttendanceDtoByType(_attendanceChannel);
        
        for (int i = 0; i < attendanceDto.Rewards.Count; i++)
        {
            _rewards[i].Refresh(attendanceDto.Rewards[i]);
        }
    }
}