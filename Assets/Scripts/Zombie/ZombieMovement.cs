using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public NavMeshAgent2D pathFinder;
    public float vicinity;
    Animator animator;
    bool isAnimating;
    public bool IsAnimating
    {
        get
        {
            return isAnimating;
        }
        set
        {
            animator.SetBool("isMoving", value);
            isAnimating = value;
        }
    }
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pathFinder = GetComponent<NavMeshAgent2D>();
        isAnimating = true;
    }

    void Update()
    {
        if(!isAnimating){
            return;
        }
        if (pathFinder == null || pathFinder.remainingDistance < vicinity)
        {
            animator.SetBool("isMoving", false);
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }
        else
        {
            Vector3 direction = (pathFinder.destination - rb.position).normalized;
            sr.flipX = direction.x < 0 ? true : false;
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
            animator.SetBool("isMoving", true);
        }
    }
}
