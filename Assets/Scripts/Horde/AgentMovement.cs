using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent2D))]
public class AgentMovement : MonoBehaviour
{
    Vector2 m_targetPos;
    NavMeshAgent2D pathfinder;

    void Awake(){
        pathfinder = GetComponent<NavMeshAgent2D>();
    }

    public void Initialize(int moveSpeed){
        pathfinder.speed = moveSpeed;
    }

    public void SetDestination(Vector2 pos){
        pathfinder.destination = pos;
    }
}
