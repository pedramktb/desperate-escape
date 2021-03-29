using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage;
    public ParticleSystem[] particleSystems;
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
        foreach(ParticleSystem particleSystem in particleSystems)
        Instantiate(particleSystem, transform.position, Quaternion.identity);
    }
}
