using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoGuyData : NPCData
{
    public int ShotgunAdditianlAmmo;
    public int MiniGunAdditinalAmmo;
    public int RockerLauncherAdditinalAmmo;
    public AmmoGuyData(int Value, int HP, int MoveSpeed, int ShotgunAdditianlAmmo, int MiniGunAdditinalAmmo, int RockerLauncherAdditinalAmmo)
    : base(Value, HP, MoveSpeed)
    {
        this.ShotgunAdditianlAmmo = ShotgunAdditianlAmmo;
        this.MiniGunAdditinalAmmo = MiniGunAdditinalAmmo;
        this.RockerLauncherAdditinalAmmo = RockerLauncherAdditinalAmmo;
    }
}
