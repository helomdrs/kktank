using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed = 5.0f;
    [SerializeField] private Vector3 offset = Vector3.up;

    private bool isActive = false;

    private void OnEnable() 
    {
        EventBusManager.Subscribe(EventBusEnum.EventName.StartMatch, OnMatchStarted);
    }

    private void OnDisable() 
    {
        EventBusManager.Unsubscribe(EventBusEnum.EventName.EndMatch, OnMatchEnded);
    }

    private void OnMatchStarted() 
    {
        isActive = true;
    }

    private void OnMatchEnded()
    {
        isActive = false;
    }

    private void Update()
    {
        if(isActive) 
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraSpeed * Time.deltaTime);
        }
    }
}
