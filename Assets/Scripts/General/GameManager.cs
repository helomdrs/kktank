using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int matchDurationInSeconds = 30;
    private const string ENEMY_TANK_TAG = "Enemy";

    private Coroutine matchDurationCo;

    private int matchScore = 0;
    private int matchDuration = 0;
    private bool playerWon = false;

    private void OnEnable() 
    {
        EventBusManager.Subscribe<string>(EventBusEnum.EventName.TankDead, OnTankDestroyed);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe<string>(EventBusEnum.EventName.TankDead, OnTankDestroyed);
    } 

    private void Start() 
    {
        Invoke(nameof(StartMatch), 2f);
    }  

    private void StartMatch()
    {
        Debug.Log("Starting match with " + matchDurationInSeconds + " of duration");
        EventBusManager.FireEvent(EventBusEnum.EventName.StartMatch);
        matchDurationCo = StartCoroutine(CountMatchDuration());
    }

    private IEnumerator CountMatchDuration() 
    {
        while (matchDuration < matchDurationInSeconds) 
        {
            matchDuration++;
            yield return new WaitForSeconds(1);
        }

        playerWon = true;
        EndMatch();
    }

    private void EndMatch()
    {
        Debug.Log("MATCH ENDED");
        StopCoroutine(matchDurationCo);

        EventBusManager.FireEvent(EventBusEnum.EventName.EndMatch);
        EventBusManager.FireEvent<bool>(EventBusEnum.EventName.UIEndScreenUpdate, playerWon);
    }

    private void OnTankDestroyed(string tankTag) 
    {
        if(tankTag == ENEMY_TANK_TAG) 
        {
            OnEnemyDestroyed();
        } 
        else 
        {
            OnPlayerDestroyed();
        }
    }

    private void OnEnemyDestroyed() 
    {
        EventBusManager.FireEvent(EventBusEnum.EventName.SpawnEnemy);
        UpdateScore();
    }

    private void OnPlayerDestroyed()
    {
        playerWon = false;
        EndMatch();
    }

    private void UpdateScore() 
    {
        matchScore += 1;
        EventBusManager.FireEvent<int>(EventBusEnum.EventName.UIScoreUpdate, matchScore);
    }
}
