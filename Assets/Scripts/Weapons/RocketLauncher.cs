using UnityEngine;
public class RocketLauncher : Weapon
{
    public Transform firePoint;
    public GameObject rocketPrefab;
    public float rocketForce;
    public float delay,lastTime;
    public override void Shoot()
    {
        if(Time.time-lastTime<delay) return;
        lastTime=Time.time;
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * rocketForce, ForceMode2D.Impulse);
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
    }
}