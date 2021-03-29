using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;
public abstract class NPCBehaviour : MonoBehaviour
{
    Health m_Health;
    NPCData m_data;
    AgentMovement movement;
    
    public int IsUnderAttack {get; private set;}
    public int Value {get; private set;}

    void Awake(){
        m_Health = GetComponent<Health>();
        movement = GetComponent<AgentMovement>();
    }

    public virtual void Initialize(NPCData data){
        m_data = data;
        movement.Initialize(data.MoveSpeed);
    }

    public void SetDestination(Vector2 pos){
        movement.SetDestination(pos);
    }
}
