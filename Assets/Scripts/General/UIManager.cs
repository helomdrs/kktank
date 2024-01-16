using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthBarBG;

    private Coroutine countdownCoroutine;
    private Coroutine timerCoroutine;

    private void OnEnable() 
    {
        EventBusManager.Subscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
        EventBusManager.Subscribe(EventBusEnum.EventName.EndMatch, OnMatchEnded);

        EventBusManager.Subscribe<int>(EventBusEnum.EventName.UIMatchTimerUpdate, OnMatchTimerUpdated);
        EventBusManager.Subscribe<int>(EventBusEnum.EventName.UICountdownUpdate, OnCountdownUpdated);
        EventBusManager.Subscribe<int>(EventBusEnum.EventName.UIScoreUpdate, OnScoreUpdated);
        EventBusManager.Subscribe<int>(EventBusEnum.EventName.UIHealthUpdate, OnHealthUpdated);
        EventBusManager.Subscribe<bool>(EventBusEnum.EventName.UIEndScreenUpdate, OnEndScreenUpdated);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
        EventBusManager.Unsubscribe(EventBusEnum.EventName.EndMatch, OnMatchEnded);

        EventBusManager.Unsubscribe<int>(EventBusEnum.EventName.UIMatchTimerUpdate, OnMatchTimerUpdated);
        EventBusManager.Unsubscribe<int>(EventBusEnum.EventName.UICountdownUpdate, OnCountdownUpdated);
        EventBusManager.Unsubscribe<int>(EventBusEnum.EventName.UIScoreUpdate, OnScoreUpdated);
        EventBusManager.Unsubscribe<int>(EventBusEnum.EventName.UIHealthUpdate, OnHealthUpdated);
        EventBusManager.Unsubscribe<bool>(EventBusEnum.EventName.UIEndScreenUpdate, OnEndScreenUpdated);
    }

    private void OnMatchStarted() 
    {
        ManageMainHUD(true);
    }

    private void OnMatchEnded() 
    {
        ManageMainHUD(false);
    }

    private void OnScoreUpdated(int newScore) 
    {
        scoreText.text = newScore.ToString("D2");
    }

    private void OnHealthUpdated(int newHealth) 
    {
        float healthValue = newHealth / 100f;
        healthBar.fillAmount = healthValue;
    }

    private void OnCountdownUpdated(int secondsToCountdown) 
    {
        ManageMainHUD(false);
        countdownText.gameObject.SetActive(true);
        countdownText.text = secondsToCountdown.ToString();
        countdownCoroutine = StartCoroutine(MakeCountdown(secondsToCountdown));
    }

    private IEnumerator MakeCountdown(int secondsToCountdown) 
    {
        for (int i = secondsToCountdown; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.gameObject.SetActive(false);
        StopCoroutine(countdownCoroutine);
    }

    private void OnEndScreenUpdated(bool playerWon) 
    {

    }

    private void OnMatchTimerUpdated(int matchDurationInSeconds) 
    {
        timerCoroutine = StartCoroutine(UpdateTimer(matchDurationInSeconds));
    }

    private IEnumerator UpdateTimer(int totalSeconds) 
    {
        for (int i = totalSeconds; i > 0; i--)
        {  
            float minutes = i / 60;
            float seconds = i % 60;
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1);
        }
    }

    private void ManageMainHUD(bool toEnable) 
    {
        timerText.gameObject.SetActive(toEnable);
        scoreText.gameObject.SetActive(toEnable);
        healthBar.gameObject.SetActive(toEnable);
        healthBarBG.gameObject.SetActive(toEnable);
    }

}