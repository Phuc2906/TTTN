using UnityEngine;
using TMPro;              
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [Header("Thời gian bắt đầu (giây)")]
    public int startTime = 60;

    [Header("Text hiển thị thời gian (TMP)")]
    public TMP_Text timerText;

    private int currentTime;
    private Coroutine countdownRoutine;

    private void OnEnable()
    {
        StartCountdown();
    }

    private void OnDisable()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);
    }

    public void StartCountdown()
    {
        currentTime = startTime;

        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        countdownRoutine = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (currentTime >= 0)
        {
            timerText.text = string.Format("{0:00}:{1:00}", currentTime / 60, currentTime % 60);

            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        gameObject.SetActive(false);
    }
}


