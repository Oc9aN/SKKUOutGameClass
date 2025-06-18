using System;
using System.Collections.Generic;
using System.Linq;
using Gpm.Ui;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ranking : MonoBehaviour
{
    // 랭킹들을 Refresh
    [Header("무한 스크롤")]
    [SerializeField]
    private InfiniteScroll _scroll;

    [Header("현재 플레이어 랭킹")]
    [SerializeField]
    private TextMeshProUGUI _currentRankingText;

    [SerializeField]
    private TextMeshProUGUI _currentNameText;

    [SerializeField]
    private TextMeshProUGUI _currentTimeText;

    [SerializeField]
    private TextMeshProUGUI _currentScoreText;

    [Header("정렬 기준")]
    [SerializeField]
    private TMP_Dropdown _rankingSortBy;

    private List<RankingSlotData> _rankingData;

    private void Start()
    {
        _rankingData = new List<RankingSlotData>();

        _rankingSortBy.onValueChanged.AddListener(DropdownValueChanged);

        Refresh();
    }

    private void Refresh()
    {
        // DTO정보를 List로 받아서 UI데이터로 변환 후 전달
        List<RankingDTO> rankings = RankingManager.Instance.Rankings;

        for (int i = 0; i < rankings.Count; i++)
        {
            if (_rankingData.Count <= i)
            {
                _rankingData.Add(new RankingSlotData(rankings[i]));
                _scroll.InsertData(_rankingData.Last());
            }

            // 인게임 중 바뀌는 값만 수정해서 전달
            _rankingData[i].SetRankingDTOData(rankings[i]);
            _scroll.UpdateData(_rankingData[i]);
        }

        var currentRanking = RankingManager.Instance.MyRanking;
        // 개인 랭킹 정보 표시
        _currentRankingText.text = currentRanking.Rank.ToString();
        _currentNameText.text = currentRanking.Nickname;
        _currentTimeText.text = FloatToFormattedTime(currentRanking.Time);
        _currentScoreText.text = currentRanking.Score.ToString();
    }

    private void DropdownValueChanged(int index)
    {
        switch (_rankingSortBy.options[index].text)
        {
            case "점수":
            {
                RankingManager.Instance.Sort(ERankingSortBy.Score);
                break;
            }
            case "생존시간":
            {
                RankingManager.Instance.Sort(ERankingSortBy.Time);
                break;
            }
        }

        Refresh();
    }
    
    string FloatToFormattedTime(float timeInSeconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(timeInSeconds);

        // 시간, 분, 초를 두 자리 숫자로 포맷팅합니다.
        // d2는 숫자를 두 자리로 채우고, 필요하면 앞에 0을 붙입니다.
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds);
    }
}