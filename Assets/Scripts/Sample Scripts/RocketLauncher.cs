using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Toastpocalupse/Weapons/Rocket Launcher")]
public class RocketLauncher : Weapons
{
    [SerializeField] float explosionRadius;
    [SerializeField] float maxExplosionForce;
    [SerializeField] float maxDemage;
    public override void ShootWeapon(Vector2 direction)
    {
        base.ShootWeapon(direction);
        GameObject projectile = Instantiate(projectilePrefap, GameManager.instance.Player.weaponPos[GameManager.instance.Player.weaponEquiped].Find("GunEnd").position, Quaternion.FromToRotation(Vector3.right, direction));
        Rocket projectileRef = projectile.GetComponent<Rocket>();
        projectileRef.projectileDestructionTime = projectileDestructionTime;
        projectileRef.explosionRadius = explosionRadius;
        projectileRef.maxExplosionForce = maxExplosionForce;
        projectileRef.maxDemage = maxDemage;
        projectileRef.Shoot(projectileForce, direction);
    }
}
