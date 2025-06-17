using System;
using System.Collections.Generic;
using System.Linq;
using Gpm.Ui;
using TMPro;
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

    private RankingType _sortBy;

    private void Start()
    {
        _rankingData = new List<RankingSlotData>();

        _rankingSortBy.onValueChanged.AddListener(DropdownValueChanged);

        Refresh();
    }

    private void Refresh()
    {
        // DTO정보를 List로 받아서 UI데이터로 변환 후 전달
        List<RankingEntryDTO> rankings = RankingManager.Instance.GetTopRankings(_sortBy);

        for (int i = 0; i < rankings.Count; i++)
        {
            if (_rankingData.Count <= i)
            {
                _rankingData.Add(new RankingSlotData(i + 1, rankings[i].PlayerNickname, rankings[i].SurvivalTimeSeconds, rankings[i].Score));
                _scroll.InsertData(_rankingData.Last());
            }

            // 인게임 중 바뀌는 값만 수정해서 전달
            _rankingData[i].Name = rankings[i].PlayerNickname;
            _rankingData[i].Time = rankings[i].SurvivalTimeSeconds;
            _rankingData[i].Score = rankings[i].Score;
            _scroll.UpdateData(_rankingData[i]);
        }

        var currentRanking = RankingManager.Instance.Get();
        // 개인 랭킹 정보 표시
        _currentRankingText.text = currentRanking.PlayerNickname;
        _currentNameText.text = currentRanking.PlayerNickname;
        _currentTimeText.text = FloatToFormattedTime(currentRanking.SurvivalTimeSeconds);
        _currentScoreText.text = currentRanking.Score.ToString();
    }

    private void DropdownValueChanged(int index)
    {
        switch (_rankingSortBy.options[index].text)
        {
            case "점수":
            {
                _sortBy = RankingType.KillPoint;
                break;
            }
            case "생존시간":
            {
                _sortBy = RankingType.SurvivalTime;
                break;
            }
        }
        Debug.Log(_sortBy);
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