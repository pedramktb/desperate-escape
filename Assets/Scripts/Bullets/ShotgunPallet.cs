using UnityEngine;

public class ShotgunPallet : MonoBehaviour
{
    public float damage;
    public GameObject hitEffect;
    public float effectDuration;
    void OnCollisionEnter2D(Collision2D collision){
        GameObject effect = Instantiate(hitEffect,transform.position,Quaternion.identity);
        Destroy(effect,effectDuration);
        Destroy(gameObject);
    }
}
