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
            currentTime--;
            LinkTimeToTimer();
        }
        GameManager.Instance.PlayCloseTransition(GameManager.Instance.DetermineWinner());
        //GameManager.Instance.EndRound(GameManager.Instance.DetermineWinner());

    }
    private void LinkTimeToTimer()
    {
        _timerText.text = currentTime.ToString();
    }
}
