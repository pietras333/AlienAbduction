using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform attractor;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 2f, -5f);

    private void Update()
    {
        if (target == null || attractor == null)
            return;

        // Calculate the desired position based on target's position and offset
        Vector3 upDirection = (target.position - attractor.position).normalized;
        Vector3 rightDirection = Vector3.Cross(target.forward, upDirection).normalized;
        Vector3 forwardDirection = Vector3.Cross(upDirection, rightDirection);

        Vector3 desiredPosition =
            target.position
            + upDirection * offset.y
            + forwardDirection * offset.z
            + rightDirection * offset.x;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            speed * Time.deltaTime
        );

        // Make the camera look at the target
        transform.LookAt(target.position, upDirection);
    }
}
