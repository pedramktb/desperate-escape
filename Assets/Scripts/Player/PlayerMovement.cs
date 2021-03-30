using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private float m_moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sr;
    [SerializeField] GameObject handRotatePoint;
    [SerializeField] GameObject hand;
    [HideInInspector] public bool canMove;
    Vector2 movement;
    Vector2 mousePos; 
    Vector2 handscale;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        handscale = hand.transform.localScale;
    }

    public void Initialize(float moveSpeed){
        m_moveSpeed = moveSpeed;
    }

    void Update()
    {
        if(!canMove)
            return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate()
    {
        if (movement == Vector2.zero) animator.SetBool("isRunning", false);
        else animator.SetBool("isRunning", true);
        rb.MovePosition(rb.position + movement * m_moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handRotatePoint.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (angle > -90 && angle < 90)
        {
            sr.flipX = false;
            hand.transform.localScale = new Vector2(handscale.x, handscale.y);
        }
        else
        {
            sr.flipX = true;
            hand.transform.localScale = new Vector2(handscale.x, handscale.y * -1);
        }
    }
}