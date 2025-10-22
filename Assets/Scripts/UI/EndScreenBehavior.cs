using TMPro;
using UnityEngine;

public class EndScreenBehavior : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnimator;
    [SerializeField] private TMP_Text _winnerText;
    [SerializeField] private TMP_Text _winnerQuoteText;
    private void Start()
    {
        _transitionAnimator.Play("OpenAnimation");
        _winnerText.text = StaticData.GetWinner().Name + "'s " + StaticData.GetWinner().Fighter.Name + "\n WON!";
        _winnerQuoteText.text = StaticData.GetWinner().Fighter.WinQuotes[Random.Range(0, StaticData.GetWinner().Fighter.WinQuotes.Length)];

        StaticData.ResetGame();
    }
}
