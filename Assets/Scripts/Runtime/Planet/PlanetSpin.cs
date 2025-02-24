using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpin : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
