using System;

[Serializable]
public class AttendanceSaveData
{
    public EAttendanceChannel AttendanceChannel;
    public string LastReceivedDate = DateTime.MinValue.ToString("yyyy-MM-dd"); // 최근 보상 수령 일자
    public int ConsecutiveCount = 1;                                           // 연속 보상 수령 횟수
    public int AttendanceCount = 0;                                            // 출석 횟수 (1부터)
}