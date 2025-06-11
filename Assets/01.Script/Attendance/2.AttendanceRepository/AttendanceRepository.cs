using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_KEY = nameof(AttendanceRepository);

    public void Save(List<AttendanceDTO> attendance, string email)
    {
        AttendanceSaveDataList datas = new AttendanceSaveDataList();
        datas.Attendances = attendance.ConvertAll(data => new AttendanceSaveData
        {
            AttendanceChannel = data.AttendanceChannel,
            LastReceivedDate = data.LastReceivedDate,
            ConsecutiveCount = data.ConsecutiveCount,
            DeadlineDate = data.DeadlineDate,
            AttendanceCount = data.AttendanceCount,
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + "_" + email, json);
    }

    public List<AttendanceSaveData> Load(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + "_" + email))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + "_" + email);
        AttendanceSaveDataList datas = JsonUtility.FromJson<AttendanceSaveDataList>(json);

        return datas.Attendances;
    }
}

[Serializable]
public class AttendanceSaveDataList
{
    public List<AttendanceSaveData> Attendances;
}