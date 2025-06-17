using System;
using System.Collections.Generic;

public class Stage
{
    // 루트 에그리거트
    // 스테이지에 따라 바뀌는 정보를 관리
    public int LevelNumber { get; private set; }
    public int SubLevelNumber => _currentLevel.CurrentLevel;
    private StageLevel _currentLevel;
    public StageLevel CurrentLevel => _currentLevel; // TODO: DTO
    private float _progressTime;
    public List<StageLevel> Levels { get; private set; } = new List<StageLevel>();

    public Stage(int levelNumber, int subLevelNumber, float progressTime, List<StageLevelSO> levels)
    {
        if (levelNumber < 0)
        {
            throw new Exception("레벨넘버가 올바르지 않습니다.");
        }

        if (subLevelNumber < 0)
        {
            throw new Exception("서브레벨넘버가 올바르지 않습니다.");
        }

        if (progressTime < 0)
        {
            throw new Exception("진행 시간이 올바르지 않습니다.");
        }

        if (levels == null)
        {
            throw new Exception("레벨 데이터들이 올바르지 않습니다.");
        }
        
        LevelNumber = levelNumber;
        _progressTime = progressTime;
        
        foreach (var level in levels)
        {
            // 서브 레벨을 start~end사이로 고정
            int sub = level.StartLevel;
            if (sub < subLevelNumber)
            {
                sub = level.EndLevel;

                if (subLevelNumber < sub)
                {
                    sub = subLevelNumber;
                }
            }
            AddLevel(new StageLevel(level, sub));
        }
        _currentLevel = Levels[LevelNumber - 1];
    }

    private void AddLevel(StageLevel level)
    {
        if (level == null)
        {
            throw new Exception("레벨이 null입니다.");
        }
        Levels.Add(level);
    }

    public void Progress(float dt, Action onDataChanged)
    {
        _progressTime += dt;

        if (_currentLevel.TryLevelUp(_progressTime))
        {
            _progressTime = 0;
            if (_currentLevel.IsClear())
            {
                LevelNumber += 1;
                _currentLevel = Levels[LevelNumber - 1];
            }
            onDataChanged?.Invoke();
        }
    }
}