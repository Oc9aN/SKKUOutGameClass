using System;

public class AttendanceReward
{
    public readonly EAttendanceChannel AttendanceChannel;
    public readonly int Day;
    public readonly int RewardAmount;
    public readonly ECurrencyType RewardCurrencyType;

    private bool _canReceive;
    public bool CanReceive => _canReceive;
    
    private bool _isTodayReward;
    public bool IsTodayReward => _isTodayReward;

    public AttendanceReward(EAttendanceChannel attendanceChannel,int day, int rewardAmount, ECurrencyType rewardCurrencyType, bool canReceive,  bool isTodayReward)
    {
        if (day <= 0)
        {
            throw new Exception("일차는 0보다 커야 합니다.");
        }
        
        if (rewardAmount <= 0)
        {
            throw new Exception("보상 수치는 0보다 커야 합니다.");
        }

        AttendanceChannel = attendanceChannel;
        Day = day;
        RewardAmount = rewardAmount;
        RewardCurrencyType = rewardCurrencyType;
        _canReceive = canReceive;
        _isTodayReward = isTodayReward;
    }

    public bool TryReceiveReward()
    {
        if (_canReceive)
        {
            _canReceive = false;
            return true;
        }

        return _canReceive;
    }
}