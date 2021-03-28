using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Toastpocalupse/Weapons/Minigun")]
public class Minigun : Weapons
{
    public float timeUntilFirstBullet;
    public float timeToCoolDownAfterReleaseOnMaxFireRate;
    public float timeToMaxFireRate;
    public float minCoolDown;
    public float timeToRegistarAsFire1Release;
    //public float maxTurnSpeed;
    //public float minTurnSpeed;
    bool isShootFrame = false;
    float tempTime;
    private void OnEnable()
    {
        cooldownTime = timeUntilFirstBullet;
        nextFireTime = 0;
    }
    public override void ShootWeapon(Vector2 direction)
    {
        isShootFrame = true;
        base.ShootWeapon(direction);
        GameObject projectile = Instantiate(projectilePrefap, GameManager.instance.Player.weaponPos[GameManager.instance.Player.weaponEquiped].Find("GunEnd").position, Quaternion.FromToRotation(Vector3.right, direction));
        Projectile projectileRef = projectile.GetComponent<Projectile>();
        projectileRef.projectileDestructionTime = projectileDestructionTime;
        projectileRef.Shoot(projectileForce, direction);
    }
    public override void Update()
    {
        base.Update();
        if (isShootFrame)
        {
            isShootFrame = false;
            tempTime = Time.fixedTime;
            cooldownTime -= (timeUntilFirstBullet - minCoolDown) / timeToMaxFireRate;
        }
        else if (tempTime + timeToRegistarAsFire1Release < Time.fixedTime - cooldownTime)
            cooldownTime += ((timeUntilFirstBullet - minCoolDown) / timeToCoolDownAfterReleaseOnMaxFireRate) * tempTime * Time.deltaTime;
        if (cooldownTime >= timeUntilFirstBullet)
        {
            cooldownTime = timeUntilFirstBullet;
            weaponAnimator.SetBool("isTurning", false);
        }
        else
            weaponAnimator.SetBool("isTurning", true);

        if (cooldownTime <= minCoolDown)
            cooldownTime = minCoolDown;
        weaponAnimator.speed = 1f / cooldownTime;
    }
    public override void ResetCooldown()
    {
        base.ResetCooldown();
        cooldownTime = timeUntilFirstBullet;
    }
}
