using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Toastpocalupse/Weapons/Pistol")]
public class Pistol : Weapons
{
    public override void ShootWeapon(Vector2 direction)
    {
        base.ShootWeapon(direction);
        AnimationTrigger();
        GameObject projectile = Instantiate(projectilePrefap, GameManager.instance.Player.weaponPos[GameManager.instance.Player.weaponEquiped].Find("GunEnd").position, Quaternion.FromToRotation(Vector3.right,direction));
        Projectile projectileRef = projectile.GetComponent<Projectile>();
        projectileRef.projectileDestructionTime = projectileDestructionTime;
        projectileRef.Shoot(projectileForce, direction);
    }
}
