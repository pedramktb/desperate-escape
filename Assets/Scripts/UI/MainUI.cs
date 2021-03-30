using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityCore.GameSystems;

public class MainUI : MonoBehaviour
{
    Health playerHealthRef;
    PlayerBehaviour playerRef;
    HordeController hordeRef;
    PlayerShooting playerShootingRef;
    [SerializeField] TMPro.TextMeshProUGUI killsRef;
    [SerializeField] TMPro.TextMeshProUGUI teamValueRef;
    [SerializeField] TMPro.TextMeshProUGUI ammoRef;
    [SerializeField] Slider hpBarRef;
    [SerializeField] Slider armorBarRef;

    public void Initialize(PlayerBehaviour playerRef, HordeController hordeRef)
    {
        this.playerRef = playerRef;
        this.hordeRef = hordeRef;
        playerHealthRef = playerRef.GetComponent<Health>();
        playerShootingRef = playerRef.GetComponent<PlayerShooting>();
    }

    private void FixedUpdate()
    {
        killsRef.text = $"Kills: {playerRef.currentKills}";
        teamValueRef.text = $"Team Value: {hordeRef.GetTeamValue()}";
        string temp = "";
        switch (playerShootingRef.CurrentWeapon)
        {
            case "Pistol":
                temp = "Infinite";
                break;
            case "Minigun":
                temp = playerShootingRef.CurrentMinigunAmmo.ToString();
                break;
            case "Shotgun":
                temp = playerShootingRef.CurrentShotgunAmmo.ToString();
                break;
            case "Rocket Launcher":
                temp = playerShootingRef.CurrentRockerlauncherAmmo.ToString();
                break;
            default:
                break;
        }
        ammoRef.text = $"Ammo: {temp}";
        hpBarRef.value = playerHealthRef.GetHealthRatio();
        armorBarRef.value = playerHealthRef.GetShieldRatio();
    }


}
