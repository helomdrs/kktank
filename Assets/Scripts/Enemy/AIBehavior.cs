using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] private float behaviorUpdateRate = 0.2f;
    
    private GameObject player;
    private TankBehavior tankBehavior;
    private NavMeshAgent agent;
    private Coroutine behaviorCo;

    private bool isActive = false;

    private void OnEnable() 
    {
        EventBusManager.Subscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
    }

    private void OnMatchStarted()
    {
        StartEnemy();
    }

    public void StartEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
        tankBehavior = GetComponent<TankBehavior>();
        tankBehavior.InitBehavior();

        if(player == null) 
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(player) 
        {
            isActive = true;
            behaviorCo = StartCoroutine(EnemyBehavior());
        } 
        else 
        {
            Debug.LogWarning("Tank didnt found player!");
        }
    }

    //Connect this to an event later
    // public void StopEnemy() 
    // {
    //     isActive = false;
    //     StopCoroutine(behaviorCo);
    // }

    private void OnDestroy() 
    {
        isActive = false;
        StopCoroutine(behaviorCo);
    }

    private IEnumerator EnemyBehavior()
    {
        while(isActive) 
        {
            if(CheckForPlayer()) 
            {
                FollowPlayer();
                AimAtPlayer();
                ShootAtPlayer();
            }
           
            yield return new WaitForSeconds(behaviorUpdateRate);
        }
    }

    private bool CheckForPlayer() 
    {
        if(player == null) 
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        return player != null;
    }

    private void FollowPlayer()
    {
        agent.destination = player.transform.position;
        RotateToPlayer();
    }

    private void RotateToPlayer() 
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), 1f * Time.deltaTime);
    }

    private void AimAtPlayer() 
    {
        tankBehavior.RotateTurret(player.transform.position);
    }

    private void ShootAtPlayer()
    {
        tankBehavior.TankShoot();
    }
}
