using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;
public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector] public int currentKills;
    PlayerMovement m_playerMovement;
    PlayerShooting m_playerShooting;
    SpriteRenderer spriteRenderer;
    Health m_health;
    bool isFlashing;
    PlayerData m_playerData;
    bool canMove = false;
    public bool AreActionsAllowed {
        get{
            return canMove;
        }
        set{
            m_playerMovement.canMove =value;
            m_playerShooting.canOperate =value;
            canMove = value;
        }
    }
    void Awake(){
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerShooting = GetComponent<PlayerShooting>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_health = GetComponent<Health>();
        isFlashing = false;
        currentKills = 0;
    }

    public void InitializePlayer(PlayerData playerData){
        m_playerData = playerData;
        AreActionsAllowed = false;
        m_playerMovement.Initialize(playerData.Speed);
        m_health.SetMaxHealth(playerData.MaxHP);
        m_health.SetMaxShield(playerData.MaxShield);
        m_health.AddShield(playerData.Shield);
        m_playerShooting.Initialize(playerData.MaxShotgunAmmo,playerData.MaxRockerLauncherAmmo,playerData.MaxMinigunAmmo);
        m_health.OnDamaged += OnDamaged;
        m_health.OnDeath += OnDeath;
    }

    private void OnDamaged(Health health, float amount, GameObject source)
    {
        StartCoroutine(Flash());
    }

    private void OnDeath(Health health)
    {
        
    }

    IEnumerator Flash()
    {
        if (isFlashing)
            yield return null;
        else
        {
            Color defaultColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            isFlashing = true;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = defaultColor;
            isFlashing = false;
        }
    }
}
