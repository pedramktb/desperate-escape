using UnityEngine;
public class Pistol : Weapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public Animator animator;
    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
