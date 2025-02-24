using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private Transform firePoint;

    public bool isShooting;
    private List<Transform> enemies = new List<Transform>();

    public void Update()
    {
        foreach (Transform enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
                continue;
            }
            enemy.SetParent(firePoint);
            enemy.transform.localPosition = Vector3.Slerp(
                enemy.transform.localPosition,
                Vector3.zero,
                speed * Time.deltaTime
            );

            if (enemy.transform.localPosition == Vector3.zero)
            {
                GameObject e = enemy.gameObject;
                enemies.Remove(enemy);
                GameManager.Instance.AddScore(e.GetComponent<Enemy>().score);
                Destroy(e.gameObject);
            }
        }
    }

    public void Reset()
    {
        enemies.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (isShooting)
            {
                if (!enemies.Contains(other.transform))
                {
                    enemies.Add(other.transform);
                }
            }
        }
    }
}
