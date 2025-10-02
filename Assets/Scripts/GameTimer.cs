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

        GameManager.Instance.EndGame(GameManager.Instance.DetermineWinner());

    }
    private void LinkTimeToTimer()
    {
        int minutes = currentTime / 60;
        int seconds = currentTime % 60;

        _timerText.text = minutes + " : " + seconds;
    }
}
