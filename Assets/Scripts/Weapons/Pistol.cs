using UnityEngine;
public class Pistol : Weapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float delay,lastTime;
    public Animator animator;
    public override void Shoot()
    {
        if(Time.time-lastTime<delay) return;
        lastTime=Time.time;
        animator.SetBool("Shoot", true);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}