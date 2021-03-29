using UnityEngine;

public class MinigunBullet : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }
}
