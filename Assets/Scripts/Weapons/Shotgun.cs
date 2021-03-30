using UnityEngine;
public class Shotgun : Weapon
{
    public Transform firePoint;
    public GameObject palletPrefab;
    public float palletForce, delay, palletDiversityAngle, palletDiversityDistance;
    public int palletCount;
    public Animator animator;
    float lastTime;
    public override void Shoot()
    {
        if (Time.time - lastTime < delay) return;
        lastTime = Time.time;
        AudioManager.instance.PlaySound("ShotgunShot");
        animator.SetTrigger("Shoot");
        GameObject[] pallets = new GameObject[palletCount];
        for (int i = 0; i <= palletCount - 1; i++)
        {
            pallets[i] = Instantiate(palletPrefab, firePoint.position + new Vector3(-firePoint.position.normalized.y, firePoint.position.normalized.x, 0) * (i % 2 == 1 ? (i / 2 + 1) * palletDiversityDistance : (i / 2) * -palletDiversityDistance), firePoint.rotation);
            Rigidbody2D rb = pallets[i].GetComponent<Rigidbody2D>();
            rb.AddForce((firePoint.right + new Vector3(0, Mathf.Sin((i % 2 == 1 ? (i / 2 + 1) * palletDiversityAngle : (i / 2) * -palletDiversityAngle)*Mathf.Deg2Rad), 0)) * palletForce, ForceMode2D.Impulse);
        }
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
    }
}