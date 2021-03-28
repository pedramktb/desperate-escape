using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement,mousePos,hndscale;
    public Animator animator;
    public Camera cam;
    public SpriteRenderer sr;
    public GameObject rp,hnd;
    void Start(){
        hndscale=hnd.transform.localScale;
    }
    void Update(){
        movement.x=Input.GetAxisRaw("Horizontal");
        movement.y=Input.GetAxisRaw("Vertical");
        mousePos=cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate(){
        if(movement==Vector2.zero) animator.SetBool("isRunning",false);
        else animator.SetBool("isRunning", true);
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        Vector2 lookDir=mousePos-rb.position;
        float angle=Mathf.Atan2(lookDir.y,lookDir.x)*Mathf.Rad2Deg;
        rp.transform.rotation=Quaternion.Euler(0,0,angle);
        if(angle>-90 && angle<90) {
            sr.flipX=false;
            hnd.transform.localScale=new Vector2(hndscale.x,hndscale.y);
        }
        else {
            sr.flipX=true;
            hnd.transform.localScale=new Vector2(hndscale.x,hndscale.y*-1);
        }
    }
}