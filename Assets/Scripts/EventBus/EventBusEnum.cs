using UnityEngine;

public class EventBusEnum : MonoBehaviour
{
    public enum EventName 
    {
        StartMatch,
        EndMatch,

        UIScoreUpdate,
        UIHealthUpdate,
        UIEndScreenUpdate,
        UICountdownUpdate,

        HitEffect,
        DamageEffect,
        ShootEffect,
        DeathEffect,

        TankDead,
        SpawnEnemy,
    }
}
