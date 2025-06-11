using System;
using System.Collections.Generic;

[Serializable]
public class AttendanceDTO
{
    public EAttendanceChannel AttendanceChannel;
    public List<AttendanceRewardDTO> Rewards;
    public string LastReceivedDate;    // ISO 8601 문자열 (예: "2025-06-11")
    public int ConsecutiveCount;
    public string DeadlineDate;        // ISO 8601 문자열
    public int AttendanceCount;

    public AttendanceDTO(Attendance attendance)
    {
        AttendanceChannel = attendance.AttendanceChannel;
        Rewards = new List<AttendanceRewardDTO>();
        foreach (var reward in attendance.Rewards)
        {
            Rewards.Add(new AttendanceRewardDTO(reward));
        }

        LastReceivedDate = attendance.LastReceivedDate.ToString("yyyy-MM-dd");
        ConsecutiveCount = attendance.ConsecutiveCount;
        DeadlineDate = attendance.DeadlineDate.ToString("yyyy-MM-dd");
        AttendanceCount = attendance.AttendanceCount;
    }
    
    public AttendanceDTO(
        EAttendanceChannel attendanceChannel,
        List<AttendanceRewardDTO> rewards,
        string lastReceivedDate,
        int consecutiveCount,
        string deadlineDate,
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