using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WaveTrigger : MonoBehaviour
{
    [SerializeField]ZombieSpawner zombieSpawner;

    public void Initialize(ZombieSpawner zombieSpawner)
    {
        this.zombieSpawner = zombieSpawner;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            zombieSpawner.StartWave();
        }
    }

    private void OnDrawGizmos()
    {
        if (zombieSpawner != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, zombieSpawner.transform.position);
        }
    }
}


