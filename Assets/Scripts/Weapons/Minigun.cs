using UnityEngine;
using System.Collections;
public class Minigun : Weapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float delay;
    public Animator animator;
    public override void Shoot()
    {
        StopAllCoroutines();
        StartCoroutine("ShootBullet");
    }
    IEnumerator ShootBullet()
    {
        while (Input.GetButton("Fire1"))
        {
            animator.SetBool("isTurning", true);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(delay);
            ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
            particleSystem.Play();
        }
        animator.SetBool("isTurning", false);
    }
}