using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_Level : MonoBehaviour
{
    [SerializeField]
    private LevelSlotInfoSO _levelSlotInfoSo;
    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private List<UI_LevelSlot> _levelSlots;

    private List<Coroutine> _coroutines;
    
    private int _slotInfoIndex; // 지금 보여주는 최소 인덱스

    private void Start()
    {
        LevelManager.Instance.OnLevelChanged += Refresh;
        
        _slotInfoIndex = 0;
        
        _coroutines = new List<Coroutine>(_levelSlots.Count);
        foreach (var slotInfo in _levelSlots)
        {
            _coroutines.Add(null);
        }

        Refresh();
    }

    private void Update()
    {
        _timeText.text = (Time.time - LevelManager.Instance.StartTime).ToString("F2");
    }

    // 현재 레벨에 맞게 UI 표시

    private void Refresh()
    {
        float levelDuration = LevelManager.Instance.Level.LevelDuration;
        int currentLevel = LevelManager.Instance.Level.CurrentLevel; // 현재 레벨
        
        // 현재 레벨에 대한 정보가 담긴 인덱스 찾기
        for (int i = 0; i < _levelSlotInfoSo.LevelSlots.Length; i++)
        {
            LevelSlot x = _levelSlotInfoSo.LevelSlots[i]; // 현재 요소를 가져옵니다.
            if (x.MinLevel <= currentLevel && x.MaxLevel >= currentLevel)
            {
                _slotInfoIndex = i;
                break; // 조건을 만족하는 첫 번째 요소를 찾으면 루프를 종료합니다.
            }
        }

        LevelSlot minSlotInfo = _levelSlotInfoSo.LevelSlots[_slotInfoIndex];

        for (int i = 0; i < _levelSlots.Count; i++)
        {
            LevelSlot slotInfo = _levelSlotInfoSo.LevelSlots[Mathf.Min(_slotInfoIndex + i, _levelSlotInfoSo.LevelSlots.Length - 1)];
            _levelSlots[i].Refresh(slotInfo.Text, slotInfo.Color);
        }
        
        int levelSize = minSlotInfo.MaxLevel - minSlotInfo.MinLevel + 1;

        _levelText.text = $"레벨: {currentLevel}";

        for (int i = 0; i < _levelSlots.Count; i++)
        {
            var rectTransform = _levelSlots[i].GetComponent<RectTransform>();
            Vector3 endPos = rectTransform.anchoredPosition;
            endPos.x -= rectTransform.rect.width / levelSize;

            if (!ReferenceEquals(_coroutines[i], null))
            {
                StopCoroutine(_coroutines[i]);
            }
            _coroutines[i] = StartCoroutine(MoveUIPanel(rectTransform, levelDuration, endPos));
        }
    }
    
    private IEnumerator MoveUIPanel(RectTransform target, float totalMoveTime, Vector3 endPos)
    {
        float elapsedTime = 0f;
        
        Vector3 startPos = target.anchoredPosition;

        while (elapsedTime < totalMoveTime)
        {
            // 경과 시간을 총 이동 시간으로 나눈 비율 (0에서 1 사이)
            float t = elapsedTime / totalMoveTime;

            // Lerp를 사용하여 현재 위치를 시작 위치와 목표 위치 사이로 보간
            target.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // 다음 프레임까지 대기
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동 완료 후 정확히 목표 위치에 설정 (오차 보정)
        target.anchoredPosition = endPos;

        if (target.anchoredPosition.x <= 0)
        {
            Vector3 newPos = target.anchoredPosition;
            newPos.x += target.rect.width * _levelSlots.Count;
            target.anchoredPosition = newPos;
            UI_LevelSlot temp = _levelSlots.First();
            _levelSlots.Add(temp);
            _levelSlots.RemoveAt(0);
        }
    }
}