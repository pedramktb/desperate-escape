using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="Toastpocalupse/Weapons/Shotgun")]
public class Shotgun : Weapons
{
    public float spreadAngle;
    public int projectileCount;
    float angleIncrement;
    float currentAngle;
    Vector3 directionToShoot;
    
  
    public override void ShootWeapon(Vector2 direction)
    {
        base.ShootWeapon(direction);
        AnimationTrigger();
        angleIncrement = spreadAngle / (projectileCount - 1f);
        currentAngle = angleToDirection - (spreadAngle / 2f);
        for (int i = 0; i < projectileCount; i++)
        {
            directionToShoot = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
            GameObject projectile = Instantiate(projectilePrefap, GameManager.instance.Player.weaponPos[GameManager.instance.Player.weaponEquiped].Find("GunEnd").position, Quaternion.identity);
            
            Projectile projectileRef = projectile.GetComponent<Projectile>();
            projectileRef.projectileDestructionTime = projectileDestructionTime;
            projectileRef.Shoot(projectileForce, directionToShoot);
            currentAngle += angleIncrement;
        }
    }

    
}
