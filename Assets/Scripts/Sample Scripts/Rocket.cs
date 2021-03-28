using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector] public float explosionRadius;
    [HideInInspector] public float maxExplosionForce;
    [HideInInspector] public float maxDemage;
    [HideInInspector] public float projectileDestructionTime;
    [SerializeField] ParticleSystem[] explosion;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(float force, Vector2 direction)
    {
        rb.AddForce(direction * force);
        Destroy(gameObject, projectileDestructionTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(ParticleSystem particle in explosion)
        {
            ParticleSystem particleRef = Instantiate(particle, transform.position, Quaternion.FromToRotation(Vector3.forward, transform.eulerAngles));
            Destroy(particleRef.gameObject, 4f);
        }
        foreach (Collider2D collider in objectsInRadius)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if(rb!=null)
            {
                float explotionForce = Mathf.Lerp(maxExplosionForce, 0, (((Vector2)collider.transform.position - (Vector2)transform.position).magnitude) / explosionRadius);
                Vector2 direction = (Vector2)collider.transform.position - (Vector2)transform.position;
                direction = direction.normalized;
                rb.AddForce(direction * explotionForce);
                Zombie zombie = collider.GetComponent<Zombie>();
                if (zombie != null)
                    zombie.HealthChange((int)((explotionForce / maxExplosionForce) * maxDemage));
            }
        }
        Destroy(gameObject);
    }
}
