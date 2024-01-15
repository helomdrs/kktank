using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string ENEMY_TANK_TAG = "Enemy";

    private int matchScore = 0;

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
        EventBusManager.FireEvent(EventBusEnum.EventName.StartMatch);
    }

    private void OnTankDestroyed(string tankTag) 
    {
        if(tankTag == ENEMY_TANK_TAG) 
        {
            OnEnemyDestroyed();
        } 
        else 
        {
            MatchOver();
        }
    }

    private void OnEnemyDestroyed() 
    {
        EventBusManager.FireEvent(EventBusEnum.EventName.SpawnEnemy);
        UpdateScore();
    }

    private void UpdateScore() 
    {
        matchScore += 1;
        EventBusManager.FireEvent<int>(EventBusEnum.EventName.UIScoreUpdate, matchScore);
    }

    private void MatchOver()
    {
        Debug.Log("Match is over!");
    }
}
