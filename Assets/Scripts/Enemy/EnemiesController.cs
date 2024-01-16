using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private int initialAmount = 5;

    private const string ENEMY_TANK_TAG = "Enemy";
    private const string ENEMY_SPAWNPOINT_TAG = "SpawnPoint";

    private GameObject[] tankSpawnPoints;

    private void OnEnable() 
    {
        EventBusManager.Subscribe(EventBusEnum.EventName.SpawnEnemy, OnSpawnTankRequested);
        EventBusManager.Subscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
        EventBusManager.Subscribe(EventBusEnum.EventName.EndMatch, OnMatchEnded);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe(EventBusEnum.EventName.SpawnEnemy, OnSpawnTankRequested);
        EventBusManager.Unsubscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
        EventBusManager.Unsubscribe(EventBusEnum.EventName.EndMatch, OnMatchEnded);
    }

    private void Start() 
    {
        tankSpawnPoints = GameObject.FindGameObjectsWithTag(ENEMY_SPAWNPOINT_TAG);
    } 

    private void OnMatchStarted() 
    {
        for (int i = 0; i < initialAmount; i++)
        {
            OnSpawnTankRequested();
        }
    }

    private void OnMatchEnded() 
    {
        foreach (GameObject tank in GameObject.FindGameObjectsWithTag(ENEMY_TANK_TAG))
        {
            Destroy(tank);
        }
    }

    private void OnSpawnTankRequested() 
    {
        GameObject tank = Instantiate(tankPrefab, tankSpawnPoints[Random.Range(0, tankSpawnPoints.Length)].transform.position, Quaternion.identity);
        tank.GetComponent<AIBehavior>().StartEnemy();
    }
}
