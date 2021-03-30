using UnityEngine;
using UnityCore.GameSystems;

public class Rocket : MonoBehaviour
{
    public float damage, blastRadious;
    public ParticleSystem[] particleSystems;
    void ExplosionDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadious);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Zombie")
            {
                collider.gameObject.GetComponent<Health>().TakeDamage(damage, gameObject);
            }
        }
    }
    void ExplosionEffects(){
        foreach (ParticleSystem particleSystem in particleSystems)
            Instantiate(particleSystem, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySound("Explosion");
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        ExplosionDamage();
        ExplosionEffects();
        if (collider.gameObject.tag == "Zombie") Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        ExplosionDamage();
        ExplosionEffects();
        Destroy(gameObject);
    }
}
