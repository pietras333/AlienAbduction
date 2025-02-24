using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    public void Remove(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
