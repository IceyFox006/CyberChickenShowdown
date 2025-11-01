using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private int currentTime;

    private void Start()
    {
        StartTimer();
    }
    private void StartTimer()
    {
        StartCoroutine(TimerCountdown());
    }
    IEnumerator TimerCountdown()
    {
        currentTime = GameManager.Instance.GameTime;
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(GameManager.Instance.Tick);
            if (GameManager.Instance.IsTimerGoing)
            {
                currentTime--;
                LinkTimeToTimer();
            }
        }
        Player winner = GameManager.Instance.DetermineWinner();
        Player loser = GameManager.Instance.GetOpponent(winner);
        winner.CombatManager.DealDamage(loser, 999999);
        loser.GameObjectController.FighterAnimator.TriggerAnimation();
    }
    private void LinkTimeToTimer()
    {
        _timerText.text = currentTime.ToString();
    }
}
