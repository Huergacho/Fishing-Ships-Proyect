using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 offset;
    private void Update()
    {
        Vector3 desPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desPos, moveSpeed * Time.deltaTime);
        transform.position = smoothPos;
        transform.LookAt(target);
    }
}
