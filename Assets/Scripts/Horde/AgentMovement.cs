using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent2D))]
public class AgentMovement : MonoBehaviour
{
    Vector2 m_targetPos;
    NavMeshAgent2D pathfinder;
    public float vicinity;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake(){
        pathfinder = GetComponent<NavMeshAgent2D>();
    }
    
    public void Initialize(int moveSpeed){
        pathfinder.speed = moveSpeed;
    }

    public void SetDestination(Vector2 pos){
        pathfinder.destination = pos;
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pathfinder = GetComponent<NavMeshAgent2D>();
    }
    void Update()
    {
        if (pathfinder == null || pathfinder.remainingDistance < vicinity)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {   
            Vector3 direction = (pathfinder.destination - rb.position).normalized;
            sr.flipX = direction.x < 0 ? true : false;
            animator.SetBool("isMoving", true);
        }
    }
}
