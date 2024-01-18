using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] GameObject explosionVfx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] GameObject shootVfx;

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
        GameObject effectObj = Instantiate(explosionVfx, effectPosition, Quaternion.identity);
        PlayEffect(effectObj);
    }

    private void OnShootEffect(Vector3 effectPosition) 
    {
        //Debug.Log("Play shoot effect on position " + effectPosition);
        GameObject effectObj = Instantiate(shootVfx, effectPosition, Quaternion.identity);
        PlayEffect(effectObj);
    }

    private void OnHitEffect(Vector3 effectPosition) 
    {
        //Debug.Log("Play hit effect on position " + effectPosition);
        GameObject effectObj = Instantiate(hitVfx, effectPosition, Quaternion.identity);
        PlayEffect(effectObj);
    }

    private void PlayEffect(GameObject effectObj)
    {
        ParticleSystem[] particlesToPlay = effectObj.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particlesToPlay)
        {
            particle.Play();
        }
        Destroy(effectObj, particlesToPlay[0].main.duration);
    }
}
