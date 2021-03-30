using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WaveTrigger : MonoBehaviour
{
    ZombieSpawner zombieSpawner;

    public void Initialize(ZombieSpawner zombieSpawner){
        this.zombieSpawner = zombieSpawner;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "player"){
            zombieSpawner.StartWave();
        }
    }
}


