using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuyData : NPCData
{
    public int ShieldAmmountOnStart;
    public ShieldGuyData(int Value, int HP, int MoveSpeed, int ShieldAmmountOnStart) : base(Value, HP, MoveSpeed)
    {
        this.ShieldAmmountOnStart = ShieldAmmountOnStart;
    }
}
