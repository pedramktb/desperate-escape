using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Action<NPCBehaviour> OnNPCDeath;
     
    public void Awake()
    {
        if(instance== null){
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
