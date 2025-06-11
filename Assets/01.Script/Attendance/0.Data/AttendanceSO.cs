using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceData", menuName = "Attendance/Attendance Data", order = 1)]
public class AttendanceSO : ScriptableObject
{
    public EAttendanceChannel AttendanceChannel;
    public List<AttendanceRewardSO> Rewards;

    [Header("출석 기간 설정")]
    public DateTime StartDate;
    public DateTime DeadlineDate;
}