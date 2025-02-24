using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject target;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * 3f * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IDamagable>(out var damagable))
        {
            collision.transform.GetComponent<IDamagable>().TakeDamage(30f);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
