using System;
using System.Collections.Generic;

[Serializable]
public class AttendanceDTO
{
    public EAttendanceChannel AttendanceChannel;
    public DateTime StartTime;
    public List<AttendanceRewardDTO> Rewards;
    public DateTime LastReceivedDate;    // ISO 8601 문자열 (예: "2025-06-11")
    public int ConsecutiveCount;
    public DateTime DeadlineDate;        // ISO 8601 문자열
    public int AttendanceCount;

    public AttendanceDTO(Attendance attendance)
    {
        AttendanceChannel = attendance.AttendanceChannel;
        Rewards = new List<AttendanceRewardDTO>();
        foreach (var reward in attendance.Rewards)
        {
            Rewards.Add(new AttendanceRewardDTO(reward));
        }

        StartTime = attendance.StartDate;
        LastReceivedDate = attendance.LastReceivedDate;
        ConsecutiveCount = attendance.ConsecutiveCount;
        DeadlineDate = attendance.DeadlineDate;
        AttendanceCount = attendance.AttendanceCount;
    }
    
    public AttendanceDTO(
        EAttendanceChannel attendanceChannel,
        List<AttendanceRewardDTO> rewards,
        DateTime lastReceivedDate,
        int consecutiveCount,
        DateTime deadlineDate,
        int attendanceCount)
    {
        AttendanceChannel = attendanceChannel;
        Rewards = rewards;
        LastReceivedDate = lastReceivedDate;
        ConsecutiveCount = consecutiveCount;
        DeadlineDate = deadlineDate;
        AttendanceCount = attendanceCount;
    }
}