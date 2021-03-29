using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float Speed;
    public int MaxHP;
    public int MaxShield;
    public int Shield;
    public int MaxShotgunAmmo;
    public int MaxMinigunAmmo;
    public int MaxRockerLauncherAmmo;
    public int GrenadeCount;
    public int Score;
    public int Kill;
    public PlayerData(float Speed, int MaxHP, int MaxShield, int Shield, int MaxShotgunAmmo, int MaxMinigunAmmo, int MaxRockerLauncherAmmo, int GrenadeCount)
    {
        Score = 0;
        Kill = 0;
        this.Speed = Speed;
        this.MaxHP = MaxHP;
        this.MaxShield = MaxShield;
        this.Shield = Shield;
        this.MaxShotgunAmmo = MaxShotgunAmmo;
        this.MaxMinigunAmmo = MaxMinigunAmmo;
        this.MaxRockerLauncherAmmo = MaxRockerLauncherAmmo;
        this.GrenadeCount = GrenadeCount;
    }
}
