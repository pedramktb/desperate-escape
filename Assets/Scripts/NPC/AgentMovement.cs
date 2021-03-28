using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    Vector2 m_targetPos;
    public int MoveSpeed {get; private set;}

    void Awake(){
        m_rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTowards(Vector2 pos){
    
    }

    private IEnumerator MoveToPos(Vector2 pos){
        yield return null;
    }
}
