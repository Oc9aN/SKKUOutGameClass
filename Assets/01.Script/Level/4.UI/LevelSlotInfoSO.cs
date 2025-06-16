using System;
using UnityEngine;

[Serializable]
public struct LevelSlot
{
    public int MinLevel;
    public int MaxLevel;
    public Color Color;
    public string Text;
}

[CreateAssetMenu(fileName = "LevelSlotInfo", menuName = "Scriptable Objects/LevelSlotInfo")]
public class LevelSlotInfoSO : ScriptableObject
{
    // 단계별 배경과 텍스트 관리
    [SerializeField]
    private LevelSlot[] _levelSlots;
    public LevelSlot[] LevelSlots => _levelSlots;
}
