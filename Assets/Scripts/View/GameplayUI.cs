using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI diamonds;
    [SerializeField] private TextMeshProUGUI rebirth;


    public void UpdateData(PlayerData data)
    {
        gold.text = data.currencies[CurrencyType.Gold].ToString();
        diamonds.text = data.currencies[CurrencyType.Gems].ToString();
        rebirth.text = data.rebirths.ToString();
    }
}
