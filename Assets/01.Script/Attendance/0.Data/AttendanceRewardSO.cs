using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceRewardSO", menuName = "Attendance/Reward Data", order = 0)]
public class AttendanceRewardSO : ScriptableObject
{
    public int Day; // 1일부터 시작
    public int RewardAmount;
    public ECurrencyType RewardCurrencyType;
}