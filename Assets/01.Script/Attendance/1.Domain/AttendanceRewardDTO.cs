using System;

[Serializable]
public class AttendanceRewardDTO
{
    public EAttendanceChannel AttendanceChannel;
    public int Day;
    public int RewardAmount;
    public ECurrencyType RewardCurrencyType;
    public bool CanReceived;
    public bool IsTodayReward;

    public AttendanceRewardDTO(AttendanceReward reward)
    {
        AttendanceChannel = reward.AttendanceChannel;
        Day = reward.Day;
        RewardAmount = reward.RewardAmount;
        RewardCurrencyType = reward.RewardCurrencyType;
        CanReceived = reward.CanReceive;
        IsTodayReward = reward.IsTodayReward;
    }
}