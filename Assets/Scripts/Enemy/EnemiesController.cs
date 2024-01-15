using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject[] tankSpawnPoints;

    private void OnEnable() 
    {
        EventBusManager.Subscribe(EventBusEnum.EventName.SpawnEnemy, OnSpawnTankRequested);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe(EventBusEnum.EventName.SpawnEnemy, OnSpawnTankRequested);
    }

    private void Start() 
    {
        tankSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    } 

    private void OnSpawnTankRequested() 
    {
        GameObject tank = Instantiate(tankPrefab, tankSpawnPoints[Random.Range(0, tankSpawnPoints.Length)].transform.position, Quaternion.identity);
        tank.GetComponent<AIBehavior>().StartEnemy();
    }
}
