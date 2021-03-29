using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }
}
