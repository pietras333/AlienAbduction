using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float rotationSpeed = 100f;

    [SerializeField]
    private float distanceToPlanet = 3f;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Transform attractor;

    private Vector3 direction;

    private void Update()
    {
        // Keep fixed distance to attractor and move player around based on direction from input
        transform.Translate(speed * Time.deltaTime * direction);
        Vector3 upDirection = (transform.position - attractor.position).normalized;

        Vector3 playerRight = Vector3.Cross(transform.forward, upDirection).normalized;

        Vector3 playerForward = Vector3.Cross(upDirection, playerRight);

        Vector3 distance = transform.position - attractor.position;
        transform.position = attractor.position + distance.normalized * distanceToPlanet;

        Vector3 gravityUp = (transform.position - attractor.position).normalized;
        Quaternion targetRotation =
            Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void Move(Vector2 direction)
    {
        this.direction = new Vector3(direction.x, 0, direction.y).normalized;
    }
}
