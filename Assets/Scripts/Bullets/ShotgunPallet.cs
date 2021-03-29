using UnityEngine;

public class ShotgunPallet : MonoBehaviour
{
    public float damage;
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }
}
