using System;
using System.Collections.Generic;
using UnityEngine;

public class AccountRepository
{
    private const string SAVE_PREFIX = "ACCOUNT_";
    
    public void TrySave(AccountDto accounts)
    {
        AccountSaveData data = new AccountSaveData(accounts);
        
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_PREFIX + data.Email, json);
    }
    
    public AccountDto Find(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_PREFIX + email);
        AccountSaveData data = JsonUtility.FromJson<AccountSaveData>(json);

        return new AccountDto(data.Email, data.Nickname, data.Password);
    }
}

[Serializable]
public struct AccountSaveData
{
    public string Email;
    public string Nickname;
    public string Password;
    
    public AccountSaveData(AccountDto account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
}