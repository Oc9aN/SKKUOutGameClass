using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceReward : MonoBehaviour
{
    public TextMeshProUGUI DateTextUI;
    public TextMeshProUGUI AmountTextUI;
    
    private Button _receiveButton;
    private Image _receiveImage;
    
    private AttendanceRewardDTO _achievementRewardDto;
    
    public event Action OnAttendanceChanged;

    private void Awake()
    {
        _receiveButton = GetComponent<Button>();
        _receiveImage = GetComponent<Image>();
    }

    public void Refresh(AttendanceRewardDTO attendanceRewardDto)
    {
        _achievementRewardDto = attendanceRewardDto;
        DateTextUI.text = attendanceRewardDto.Day.ToString();
        AmountTextUI.text = attendanceRewardDto.RewardAmount.ToString();

        _receiveButton.interactable = attendanceRewardDto.CanReceived;
        
        _receiveImage.color = attendanceRewardDto.IsTodayReward ? Color.blue : Color.red;
    }

    public void ReceiveReward()
    {
        if (_achievementRewardDto == null)
        {
            return;
        }
        
        AttendanceManager.instance.TryReceiveReward(_achievementRewardDto);
        OnAttendanceChanged?.Invoke();
    }
}