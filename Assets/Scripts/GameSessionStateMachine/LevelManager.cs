using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityCore.StateMachine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerSpawnPoint;
    [SerializeField] GameObject HordeSpawnPoint;
    [SerializeField] GameObject HordeControllerPrefabRef;
    HordeController m_hordeController;
    HordeData m_startingHorde;


    public void InitializeLevel(HordeData data)
    {
        m_hordeController = Instantiate(HordeControllerPrefabRef,Vector2.zero,Quaternion.identity).GetComponent<HordeController>();
        m_hordeController.InitilizeHorde(data,HordeSpawnPoint.transform.position);
    }
}
