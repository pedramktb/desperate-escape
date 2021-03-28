using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons :ScriptableObject
{
    protected float nextFireTime = 0f;
    public float cooldownTime;
    public int demagePerBullet;
    protected float angleToDirection;
    [SerializeField] protected float projectileDestructionTime;
    public float projectileForce;
    public GameObject projectilePrefap;
    public GameObject weaponSpriteAndStuff;
    public float weaponSpawnAdjustValue;
    protected float lastFireTime;
    protected Animator weaponAnimator;
    [SerializeField] protected ParticleSystem particleSystem;
    private void OnEnable()
    {
        nextFireTime = 0;
    }
    public void isTimeForShoot(Vector2 direction)
    {
        direction = direction.normalized;
        if (nextFireTime <= Time.fixedTime)
        {
            lastFireTime = nextFireTime-cooldownTime;
            nextFireTime = Time.fixedTime + cooldownTime;
            
            ShootWeapon(direction);
        }
    }
    public virtual void Update()
    {
        if(weaponAnimator==null)
        {
            weaponAnimator = GameManager.instance.Player.weaponPos[GameManager.instance.Player.weaponEquiped].GetComponent<Animator>();
        }
    }
    public virtual void ShootWeapon(Vector2 direction)
    {
        angleToDirection = Vector2.Angle(Vector2.right, direction);
        if (direction.x < 0f && direction.y < 0f)
            angleToDirection = 270f - (angleToDirection - 90f);
        else if (direction.x > 0f && direction.y < 0f)
            angleToDirection = 360f - angleToDirection;
        else if (direction.x == 0f && Mathf.Approximately(direction.y, -1f))
            angleToDirection = 270;
        ShootParticles(direction);
    }
    public virtual void ResetCooldown()
    {}
    protected void AnimationTrigger()
    {
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("Shoot");
        }
    }
    protected void ShootParticles(Vector2 direction)
    {
        ParticleSystem particleRef = Instantiate(particleSystem, GameManager.instance.Player.weaponPos[GameManager.instance.Player.weaponEquiped].Find("GunEnd").position, Quaternion.FromToRotation(Vector3.forward, direction));
        Destroy(particleRef.gameObject, 2f);
    }
}
