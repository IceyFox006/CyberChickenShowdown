using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerText;
    [SerializeField] private TMP_Text _winnerQuoteText;
    [SerializeField] private Image _quoteImage;
    private void Start()
    {
        PlayerSO winner = StaticData.GetWinner();
        _winnerText.text = winner.Name;// + " WON!";//+ "'s " + winner.Fighter.Name + "\n WON!";
        _winnerQuoteText.text = winner.Fighter.WinQuotes[Random.Range(0, winner.Fighter.WinQuotes.Length)];
        _quoteImage.sprite = winner.Fighter.EndQuoteSprite;

        StaticData.ResetGame();
    }
}
