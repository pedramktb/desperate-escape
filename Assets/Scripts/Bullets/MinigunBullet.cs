using UnityEngine;
using UnityCore.GameSystems;

public class MinigunBullet : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag=="Zombie"){
            collider.gameObject.GetComponent<Health>().TakeDamage(damage,gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }
}
