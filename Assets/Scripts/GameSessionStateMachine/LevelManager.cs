using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityCore.StateMachine;
using Utility;
using UnityCore.GameSystems;

public class LevelManager : StateMachine
{
    [SerializeField] GameObject PlayerSpawnPoint;
    [SerializeField] GameObject HordeSpawnPoint;
    [SerializeField] GameObject HordeControllerPrefabRef;
    [SerializeField] GameObject PlayerPrefabRef;
    [SerializeField] UIManager UIManager;
    [SerializeField] List<ZombieSpawner> spawners;
    [SerializeField] CameraController cameraController;
    [SerializeField] WinStation winStation;

    GameManager m_gameManager;
    TimeEngine m_timeEngine;
    HordeController m_hordeController;
    HordeData m_startingHorde;
    PlayerData m_playerData;
    PlayerBehaviour m_playerRef;

    public void SetPlayer(PlayerBehaviour player)
    {
        m_playerRef = player;
        player.GetComponent<Health>().OnDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(Health health)
    {
        SetState(GameSessionState.Lost);
    }

    public void Initialize(PlayerData playerData, HordeData hordeData, ref bool showTutorial, GameManager gameManager)
    {
       
        m_timeEngine = GetComponent<TimeEngine>();
        if(m_timeEngine == null)
            m_timeEngine = gameObject.AddComponent<TimeEngine>();
        m_playerData = playerData;
        m_startingHorde = hordeData;
        m_gameManager = gameManager;

        m_hordeController = Instantiate(HordeControllerPrefabRef, Vector2.zero, Quaternion.identity).GetComponent<HordeController>();
        if (showTutorial)
        {
            SetState(GameSessionState.Tutorial);
            showTutorial = false;
            return;
        }
        SetState(GameSessionState.Intro);
    }



    public void SetState(GameSessionState state)
    {
        toggleStateUpdate = false;
        CurrentState?.DeInit();
        State gameState;
        if (state == GameSessionState.Intro)
        {
            gameState = new IntroState(this, UIManager,
            m_timeEngine,
            m_hordeController,
            HordeSpawnPoint.transform.position,
            PlayerSpawnPoint.transform.transform.position,
            m_startingHorde,
            m_playerData,
            PlayerPrefabRef,
            spawners,
            cameraController,
            m_gameManager);
        }
        else if (state == GameSessionState.Tutorial)
        {
            gameState = new TutorialState(this, UIManager);
        }
        else if (state == GameSessionState.DuringGame)
        {
            gameState = new DuringGameState(this, UIManager, m_hordeController, m_playerRef, spawners, cameraController, m_timeEngine);
        }
        else if (state == GameSessionState.Lost)
        {
            gameState = new LostState(this, UIManager);
        }
        else if (state == GameSessionState.Won)
        {
            gameState = new WonState(this, UIManager);
        }
        else if (state == GameSessionState.ChooseSacrifice)
        {
            gameState = new ChooseSacrificeState(this, UIManager, m_timeEngine, m_gameManager, m_playerRef, m_hordeController);
        }
        else
        {
            gameState = new NoneState();
        }
        CurrentState = gameState;
        CurrentState?.Init();
        toggleStateUpdate = true;
        Debug.Log($"switched to state {state}");
    }

    protected override void Update()
    {
        base.Update();
    }



}
