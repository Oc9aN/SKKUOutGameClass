using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelNameText;

    [SerializeField]
    private Image _levelColor;

    public void Refresh(string levelName, Color color)
    {
        _levelNameText.text = levelName;
        _levelColor.color = color;
    }
}
