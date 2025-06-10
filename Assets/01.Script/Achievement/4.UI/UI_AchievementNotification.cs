using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    public float FadeOutTime = 2f;
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        AchievementManager.instance.OnNewAchievementCanRewarded += Notification;
        
        gameObject.SetActive(false);
    }

    private void Notification(AchievementDTO achievement)
    {
        gameObject.SetActive(true);
        NameTextUI.text = achievement.Name;
        DescriptionTextUI.text = achievement.Description;

        StartCoroutine(FadeOut_Coroutine());
    }

    private IEnumerator FadeOut_Coroutine()
    {
        float time = 0f;
        float startAlpha = _canvasGroup.alpha;

        while (time < FadeOutTime)
        {
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / FadeOutTime);
            time += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}