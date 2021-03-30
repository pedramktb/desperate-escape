using UnityEngine;
public class Pistol : Weapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float delay;
    float lastTime;
    public Animator animator;
    public override void Shoot()
    {
        if(Time.time-lastTime<delay) return;
        lastTime=Time.time;
        AudioManager.instance.PlaySound("Shot");
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
    }
}