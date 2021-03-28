using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;
public class NPCBehaviour : MonoBehaviour
{
    Health m_Health;
    AgentMovement movement;
    
    public int IsUnderAttack {get; private set;}
    public int Value {get; private set;}

    void Awake(){
        m_Health = GetComponent<Health>();
        movement = GetComponent<AgentMovement>();
    }


}
