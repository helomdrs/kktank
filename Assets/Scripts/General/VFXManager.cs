using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private void OnEnable() 
    {
        EventBusManager.Subscribe<Vector3>(EventBusEnum.EventName.DeathEffect, OnDeathEffect);
        EventBusManager.Subscribe<Vector3>(EventBusEnum.EventName.ShootEffect, OnShootEffect);
        EventBusManager.Subscribe<Vector3>(EventBusEnum.EventName.HitEffect, OnHitEffect);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe<Vector3>(EventBusEnum.EventName.DeathEffect, OnDeathEffect);
        EventBusManager.Unsubscribe<Vector3>(EventBusEnum.EventName.ShootEffect, OnShootEffect);
        EventBusManager.Unsubscribe<Vector3>(EventBusEnum.EventName.HitEffect, OnHitEffect);
    }

    private void OnDeathEffect(Vector3 effectPosition) 
    {
        //Debug.Log("Play death effect on position " + effectPosition);
    }

    private void OnShootEffect(Vector3 effectPosition) 
    {
        //Debug.Log("Play shoot effect on position " + effectPosition);
    }

    private void OnHitEffect(Vector3 effectPosition) 
    {
        //Debug.Log("Play hit effect on position " + effectPosition);
    }
}
