using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject rocket;
    private int uses = 10;
    private float timeSinceLastShot = 0f;

    public void Update()
    {
        if (uses == 0 || rocket == null || Time.time - timeSinceLastShot < 5f)
        {
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, 25f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (uses > 0)
                {
                    GameObject rockett = Instantiate(
                        rocket,
                        transform.position,
                        transform.rotation
                    );
                    rockett.GetComponent<Rocket>().target = collider.gameObject;
                    uses--;
                    timeSinceLastShot = Time.time;
                }
            }
        }
    }

    [SerializeField]
    public int score;

    public void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<ObjectRemover>().Remove(this);
    }
}
