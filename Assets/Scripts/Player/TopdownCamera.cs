using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed = 5.0f;
    [SerializeField] private Vector3 offset = Vector3.up;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraSpeed * Time.deltaTime);
    }
}
