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
        winner.CombatManager.DealDamage(GameManager.Instance.GetOpponent(winner), 999999);
        //GameManager.Instance.GetOpponent(GameManager.Instance.DetermineWinner()).CurrentHP = 0;
        //GameManager.Instance.PlayCloseTransition(GameManager.Instance.DetermineWinner());
    }
    private void LinkTimeToTimer()
    {
        _timerText.text = currentTime.ToString();
    }
}
