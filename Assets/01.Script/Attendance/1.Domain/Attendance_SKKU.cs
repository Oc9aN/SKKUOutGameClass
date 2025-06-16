using System;
using System.Collections.Generic;

public class Attendance_SKKU
{
    public DateTime StartDate;
    public int DayCount;
    public DateTime LastAttendanceDate;

    public List<AttendanceReward_SKKU> Rewards;

    public Attendance_SKKU(DateTime startDate, int dayCount, DateTime lastAttendanceDate)
    {
        if (startDate == new DateTime())
        {
            throw new Exception("출석 시작일이 지정되지 않았습니다.");
        }

        if (dayCount <= 0)
        {
            throw new Exception("");
        }

        if (lastAttendanceDate == new DateTime())
        {
            throw new Exception("출석 시작일이 지정되지 않았습니다.");
        }

        if (lastAttendanceDate < startDate)
        {
            throw new Exception("출서일은 이벤트 시작일보다 작을 수 없습니다.");
        }
        
        StartDate = startDate;
        DayCount = dayCount;
        LastAttendanceDate = lastAttendanceDate;
    }

    public void Check(DateTime date)
    {
        if (date == new DateTime())
        {
            throw new Exception("출석 체크하는 date가 지정되지 안았습니다.");
        }

        if (LastAttendanceDate.Year <= date.Year &&
            LastAttendanceDate.Month <= date.Month &&
            LastAttendanceDate.Day < date.Day)
        {
            DayCount += 1;
            LastAttendanceDate = date;
        }
    }
}