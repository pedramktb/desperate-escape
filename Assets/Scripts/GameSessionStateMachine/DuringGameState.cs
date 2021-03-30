using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.StateMachine;
using Utility;
using UnityCore.GameSystems;


public class DuringGameState : State
{
    LevelManager m_levelManager;
    UIManager m_UIManager;
    HordeController m_hordeController;
    PlayerBehaviour m_playerRef;
    bool m_canOperate;
    CameraController m_cameraController;
    List<ZombieSpawner> m_spawners;
    TimeEngine m_timeEngine;
    public DuringGameState(
        LevelManager levelManager,
        UIManager UIManager,
        HordeController hordeController,
        PlayerBehaviour playerRef,
        List<ZombieSpawner> spawners,
        CameraController cameraController,
        TimeEngine timeEngine)
    {
        m_canOperate = false;
        m_levelManager = levelManager;
        m_UIManager = UIManager;
        m_hordeController = hordeController;
        m_playerRef = playerRef;
        m_spawners = spawners;
        m_cameraController = cameraController;
        m_timeEngine = timeEngine;
    }
    public override void Init()
    {
        m_playerRef.AreActionsAllowed = true;
        m_canOperate = true;
        foreach (var i in m_spawners)
        {
            i.StartSpawning();
            i.OnWaveStarted += OnWaveStarted;
            i.onZombieDeath += OnZombieDeath;
        }
        m_playerRef.GetComponent<Health>().OnDeath += OnPlayerDeath;
        m_UIManager.ShowMainUI();
        m_hordeController.OnAllNPCsDeath += OnAllNPCDeath;
        m_hordeController.OnAllNPCsSafety += OnNPCsSafety;
    }
    public override void DeInit()
    {
        m_playerRef.AreActionsAllowed = false;
        m_canOperate = false;
        m_UIManager.HideMainUI();
        m_playerRef.GetComponent<Health>().OnDeath -= OnPlayerDeath;
        m_hordeController.OnAllNPCsDeath -= OnAllNPCDeath;
        m_hordeController.OnAllNPCsSafety -= OnNPCsSafety;

        foreach (var i in m_spawners)
        {
            i.StopAndDestroyEverything();
            i.OnWaveStarted -= OnWaveStarted;
            i.onZombieDeath -= OnZombieDeath;
        }
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

    public void OnWaveStarted()
    {
        m_UIManager.ShowWavePanel();
        m_timeEngine.StartTimer(new Timer(3, "hide wave panel", m_UIManager.HideWavePanel));
    }

    public void OnPlayerDeath(Health health)
    {
        m_levelManager.SetState(GameSessionState.Lost);
    }

    public void OnAllNPCDeath()
    {
        m_levelManager.SetState(GameSessionState.Lost);
    }

    public void OnZombieDeath()
    {
        m_playerRef.currentKills++;
    }

    public void OnNPCsSafety()
    {
        m_levelManager.SetState(GameSessionState.ChooseSacrifice);
    }

}
