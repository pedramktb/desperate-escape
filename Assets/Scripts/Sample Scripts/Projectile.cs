using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector] public float projectileDestructionTime;
    bool hasDealtDemage = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(float force,Vector2 direction)
    {
        rb.AddForce(direction * force);
        Destroy(gameObject, projectileDestructionTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //it collides with a child of zombie but it works!?????
        Zombie zombie = collision.gameObject.GetComponent<Zombie>();
        if (zombie != null&&!hasDealtDemage)
        {

            zombie.HealthChange(GameManager.instance.Player.weapons[GameManager.instance.Player.weaponEquiped].demagePerBullet);
            hasDealtDemage = true;
            Destroy(gameObject);
        }
    }
}
