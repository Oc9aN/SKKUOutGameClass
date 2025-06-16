using System;

public class AttendanceReward_SKKU
{
    public ECurrencyType CurrencyType;
    public int Amount;
    public bool Claimed;

    public AttendanceReward_SKKU(ECurrencyType currencyType, int amount, bool claimed)
    {
        if (Amount < 0)
        {
            throw new Exception("출석 보상은 0보다 작을 수 없습니다.");
        }
        
        CurrencyType = currencyType;
        Amount = amount;
        Claimed = claimed;
    }
}