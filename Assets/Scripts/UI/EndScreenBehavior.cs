using TMPro;
using UnityEngine;

public class EndScreenBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerText;
    [SerializeField] private TMP_Text _winnerQuoteText;
    private void Start()
    {
        _winnerText.text = "Player " + StaticData.WinnerID + "'s " + StaticData.WinningFighter.Name + "\n WON!";
        _winnerQuoteText.text = StaticData.WinningFighter.WinQuotes[Random.Range(0, StaticData.WinningFighter.WinQuotes.Length)];
    }
}
