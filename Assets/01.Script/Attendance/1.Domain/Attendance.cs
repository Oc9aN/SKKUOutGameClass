using System;
using System.Collections.Generic;

[Serializable]
public enum EAttendanceChannel
{
    Normal,
    Event_1,
    Event_2,
}

public class Attendance
{
    // 데이터
    public DateTime StartDate;  // 출석 시작일
    public readonly EAttendanceChannel AttendanceChannel;
    public readonly List<AttendanceReward> Rewards;

    public readonly DateTime DeadlineDate; // 출석 보상 기간

    // 상태
    public DateTime LastReceivedDate; // 최근 보상 수령 일자
    public int ConsecutiveCount;      // 연속 보상 수령 횟수
    private int _attendanceCount; // 출석 횟수
    public int AttendanceCount => _attendanceCount;

    public Attendance(AttendanceSO meta, AttendanceSaveData saveData, DateTime today)
    {
        if (today.Date < StartDate)
        {
            throw new Exception("출석 시작일 이전입니다.");
        }
        if (meta == null)
        {
            throw new Exception("출석 보상 정보가 없습니다.");
        }

        AttendanceChannel = meta.AttendanceChannel;

        StartDate = DateTime.Parse(meta.StartDate);
        LastReceivedDate = saveData != null ? DateTime.Parse(saveData.LastReceivedDate).Date : DateTime.MinValue.Date; // 시간 제거
        ConsecutiveCount = saveData?.ConsecutiveCount ?? 0;
        DeadlineDate = DateTime.Parse(meta.DeadlineDate);
        _attendanceCount = saveData?.AttendanceCount ?? 0;
        
        Rewards = new List<AttendanceReward>();
        for (int i = 0; i < meta.Rewards.Count; i++)
        {
            Rewards.Add(new AttendanceReward(
                AttendanceChannel,
                meta.Rewards[i].Day,
                meta.Rewards[i].RewardAmount,
                meta.Rewards[i].RewardCurrencyType,
                i >= _attendanceCount,
                _attendanceCount == i && CanReceive(today)
            ));
        }
    }

    // 보상 수령 가능 체크 후 수령
    public bool TryReceiveReward(DateTime today, int day)
    {
        if (!OnDeadline(today))
        {
            return false;
        }

        // 진행도와 일자(day)를 비교
        if (_attendanceCount + 1 != day)
        {
            return false;
        }

        // 출석을 다 한 경우
        if (_attendanceCount > Rewards.Count)
        {
            return false;
        }

        // 보상을 받을 수 없는 날짜인 경우
        var todayDate = today.Date;
        if (!CanReceive(todayDate))
        {
            return false;
        }

        // 이미 받은 보상인 경우, 배열의 0번이 1일차 보상
        if (!Rewards[_attendanceCount].TryReceiveReward())
        {
            return false;
        }

        _attendanceCount += 1;

        UpdateConsecutiveDays(todayDate);
        LastReceivedDate = todayDate;
        return true;
    }

    private void UpdateConsecutiveDays(DateTime today)
    {
        if (LastReceivedDate == today.AddDays(-1))
            ConsecutiveCount++;
        else
            ConsecutiveCount = 1;
    }

    // 하루에 2개 불가능 체크
    private bool CanReceive(DateTime today) =>
        (LastReceivedDate.Year <= today.Year &&
         LastReceivedDate.Month <= today.Month &&
         LastReceivedDate.Day < today.Day);

    private bool OnDeadline(DateTime today) =>
        DeadlineDate != today.Date;

    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(this);
    }
}