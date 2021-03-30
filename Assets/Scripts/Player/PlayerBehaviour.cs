using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;
public class PlayerBehaviour : MonoBehaviour
{
    PlayerMovement m_playerMovement;
    PlayerShooting m_playerShooting;
    Health m_health;
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
        m_health = GetComponent<Health>();
    }

    public void InitializePlayer(PlayerData playerData){
        m_playerData = playerData;
        AreActionsAllowed = false;
        m_playerMovement.Initialize(playerData.Speed);
        m_health.SetMaxHealth(playerData.MaxHP);
        m_health.SetMaxShield(playerData.MaxShield);
        m_health.AddShield(playerData.Shield);
        m_playerShooting.Initialize(playerData.MaxShotgunAmmo,playerData.MaxRockerLauncherAmmo,playerData.MaxMinigunAmmo);
    }
}
