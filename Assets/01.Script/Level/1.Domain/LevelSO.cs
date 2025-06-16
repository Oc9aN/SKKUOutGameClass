using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    [Header("레벨 기본 정보 (초기값)")]
    [SerializeField] private float initialMonsterAttack;      // 몬스터 기본 공격력
    [SerializeField] private float initialMonsterHealth;      // 몬스터 기본 체력
    [SerializeField] private float initialSpawnInterval;      // 몬스터 기본 스폰 주기(초)
    [SerializeField] private int initialMaxSpawnCount;        // 몬스터 기본 스폰 최대치
    [SerializeField] private float initialLevelDuration;      // 레벨 기본 유지 시간(초)
    [SerializeField, Range(0f, 1f)] private float initialEliteProbability; // 엘리트 기본 확률

    [Header("몬스터 증가 수치 (레벨업 시 증가값)")]
    [SerializeField] private float monsterAttackIncrease;     // 몬스터 공격력 증가값
    [SerializeField] private float monsterHealthIncrease;     // 몬스터 체력 증가값

    [Header("스폰 관련 (레벨업 시 변화값)")]
    [SerializeField] private float spawnCycleDecrease;        // 몬스터 스폰 주기 감소치(초)
    [SerializeField] private int maxSpawnCount;               // 몬스터 스폰 최대치 증가값

    [Header("레벨 설정 (레벨업 시 변화값)")]
    [SerializeField] private float levelDuration;             // 레벨 유지 시간 증가값(초)
    [SerializeField, Range(0f, 1f)] private float eliteProbability; // 엘리트 확률 증가값

    // 초기값 프로퍼티
    public float InitialMonsterAttack => initialMonsterAttack;
    public float InitialMonsterHealth => initialMonsterHealth;
    public float InitialSpawnInterval => initialSpawnInterval;
    public int InitialMaxSpawnCount => initialMaxSpawnCount;
    public float InitialLevelDuration => initialLevelDuration;
    public float InitialEliteProbability => initialEliteProbability;

    // 증가값 프로퍼티
    public float MonsterAttackIncrease => monsterAttackIncrease;
    public float MonsterHealthIncrease => monsterHealthIncrease;
    public float SpawnCycleDecrease => spawnCycleDecrease;
    public int MaxSpawnCount => maxSpawnCount;
    public float LevelDuration => levelDuration;
    public float EliteProbability => eliteProbability;
}
