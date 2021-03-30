using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.StateMachine;

public class DuringGameState : State
{
    LevelManager m_levelManager;
    UIManager m_UIManager;
    HordeController m_hordeController;
    PlayerBehaviour m_playerRef;
    bool m_canOperate;
    CameraController m_cameraController;
    List<ZombieSpawner> m_spawners;
    public DuringGameState(
        LevelManager levelManager,
        UIManager UIManager,
        HordeController hordeController,
        PlayerBehaviour playerRef,
        List<ZombieSpawner> spawners,
        CameraController cameraController)
    {
        m_canOperate = false;
        m_levelManager = levelManager;
        m_UIManager = UIManager;
        m_hordeController = hordeController;
        m_playerRef = playerRef;
        m_spawners = spawners;
        m_cameraController = cameraController;
    }
    public override void Init()
    {
        m_playerRef.AreActionsAllowed = true;
        m_canOperate = true;
        foreach (var i in m_spawners)
        {
            i.StartSpawning();
        }
    }
    public override void DeInit()
    {
        m_playerRef.AreActionsAllowed = false;
        m_canOperate = false;
    }

    public override void Update()
    {
        if (!m_canOperate)
            return;
        if (Input.GetButtonDown("Fire2"))
        {
            m_hordeController.MoveHordeTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
