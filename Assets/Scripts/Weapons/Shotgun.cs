using UnityEngine;
using Utility;

public class Shotgun : Weapon
{
    public Transform firePoint;
    public GameObject palletPrefab;
    public float palletForce, delay, palletDiversityAngle, palletDiversityDistance;
    public int palletCount;
    public Animator animator;
    TimeEngine timeEngine;
    float lastTime;

    private void Awake()
    {
        timeEngine = gameObject.AddComponent<TimeEngine>();
    }
    public override void Shoot()
    {
        if (Time.time - lastTime < delay  || PlayerShooting.instance.CurrentShotgunAmmo == 0) return;
        lastTime = Time.time;
        PlayerShooting.instance.CurrentShotgunAmmo--;
        AudioManager.instance.PlaySound("ShotgunShot");
        GameObject[] pallets = new GameObject[palletCount];
        for (int i = 0; i <= palletCount - 1; i++)
        {
            pallets[i] = Instantiate(palletPrefab, firePoint.position + new Vector3(-firePoint.position.normalized.y, firePoint.position.normalized.x, 0) * (i % 2 == 1 ? (i / 2 + 1) * palletDiversityDistance : (i / 2) * -palletDiversityDistance), firePoint.rotation);
            Rigidbody2D rb = pallets[i].GetComponent<Rigidbody2D>();
            rb.AddForce((firePoint.right + new Vector3(0, Mathf.Sin((i % 2 == 1 ? (i / 2 + 1) * palletDiversityAngle : (i / 2) * -palletDiversityAngle)*Mathf.Deg2Rad), 0)) * palletForce, ForceMode2D.Impulse);
        }
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        timeEngine.StartTimer(new Timer(0.2f, "HandShotgunEffects", HandShotgunEffects));
    }

    private void HandShotgunEffects()
    {
        Debug.Log("Called");
        animator.SetTrigger("Shoot");
        AudioManager.instance.PlaySound("ShotgunReload");
    }
}