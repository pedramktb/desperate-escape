using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;

public class ZombieBehaviour : MonoBehaviour
{
    NavMeshAgent2D pathfinder;
    ZombieMovement zombieMovement;
    Health health;
    GameObject objectToFollow;
    public bool ShouldFollow
    {
        get
        {
            if(pathfinder.speed == 0)
                return false;
            return true;
        }
        set
        {
            pathfinder.speed = value == true ? moveSpeed : 0;
            zombieMovement.IsAnimating = value;
        }
    }
    [SerializeField] ZombieType type;
    [SerializeField] int demagePerAttack;
    [SerializeField] float moveSpeed;

    void Awake()
    {
        pathfinder = GetComponent<NavMeshAgent2D>();
        zombieMovement = GetComponent<ZombieMovement>();
        health = GetComponent<Health>();
    }


    public void Initialize(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow;
        pathfinder.speed = moveSpeed;
    }

    void FixedUpdate()
    {
        pathfinder.SetDestination(objectToFollow.transform.position);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            //zombieSpawner.StartWave();
        }
    }
}
