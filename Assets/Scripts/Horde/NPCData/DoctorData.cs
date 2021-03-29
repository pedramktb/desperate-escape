using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorData : NPCData
{
    public int HealAmount;
    public int HealCooldown;

    public DoctorData(int Value, int HP, int MoveSpeed, int HealAmount, int HealCooldown) : base(Value, HP, MoveSpeed)
    {
        this.HealAmount = HealAmount;
        this.HealCooldown = HealCooldown;
    }
}
