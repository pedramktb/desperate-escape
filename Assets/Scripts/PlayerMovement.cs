using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    void Update(){
        movement.x=Input.GetAxisRaw("Horizontal");
        movement.y=Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate(){
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
    }
}