using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeData
{
    public List<NPCData> startingHorde;
    public HordeData(List<NPCData> data){
        startingHorde = data;
    }
}
