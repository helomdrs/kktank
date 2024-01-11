using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float behaviorUpdateRate = 0.2f;
    
    private TankBehavior tankBehavior;
    private NavMeshAgent agent;
    private Coroutine behaviorCo;

    private bool isActive = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tankBehavior = GetComponent<TankBehavior>();

        //put this in a initialize event later, it has to have a delay time otherwise will bug
        Invoke(nameof(StartEnemy), 2f);
    }

    private void StartEnemy()
    {
        isActive = true;
        behaviorCo = StartCoroutine(EnemyBehavior());
    }

    //Connect this to an event later
    private void StopEnemy() 
    {
        isActive = false;
        StopCoroutine(behaviorCo);
    }

    private IEnumerator EnemyBehavior()
    {
        while(isActive) 
        {
            FollowPlayer();
            AimAtPlayer();
            ShootAtPlayer();

            yield return new WaitForSeconds(behaviorUpdateRate);
        }
    }

    private void FollowPlayer()
    {
        agent.destination = player.position;
        RotateToPlayer();
    }

    private void RotateToPlayer() 
    {
        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), 1f * Time.deltaTime);
    }

    private void AimAtPlayer() 
    {
        tankBehavior.RotateTurret(player.position);
    }

    private void ShootAtPlayer()
    {
        tankBehavior.TankShoot();
    }
}
