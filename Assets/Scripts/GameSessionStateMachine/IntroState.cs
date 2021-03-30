using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.StateMachine;
using Utility;
public class IntroState : State
{
    LevelManager m_levelManager;
    UIManager m_UIManager;
    TimeEngine m_timeEngine;
    HordeController m_hordeController;
    Vector2 m_playerStartPos;
    Vector2 m_hordeStartPos;
    HordeData m_hordeData;
    PlayerData m_playerData;
    GameObject m_PlayerPrefab;
    PlayerBehaviour m_playerRef;
    List<ZombieSpawner> m_spawners;
    CameraController m_cameraController;

    public IntroState(LevelManager levelManager,
        UIManager UIManager,
        TimeEngine timeEngine,
        HordeController hordeController,
        Vector2 hordeStartPos,
        Vector2 playerStartPos,
        HordeData hordeData,
        PlayerData playerData,
        GameObject playerPrefab,
        List<ZombieSpawner> spawners,
        CameraController cameraController)
    {
        m_levelManager = levelManager;
        m_UIManager = UIManager;
        m_timeEngine = timeEngine;
        m_hordeController = hordeController;
        m_playerStartPos = playerStartPos;
        m_hordeStartPos = hordeStartPos;
        m_hordeData = hordeData;
        m_playerData = playerData;
        m_PlayerPrefab = playerPrefab;
        m_spawners = spawners;
        m_cameraController = cameraController;
    }
    public override void Init()
    {
        m_hordeController.InitilizeHorde(m_hordeData, m_hordeStartPos);
        m_playerRef = GameObject.Instantiate(m_PlayerPrefab, m_playerStartPos, Quaternion.identity).GetComponent<PlayerBehaviour>();
        m_playerRef.InitializePlayer(m_playerData);
        m_levelManager.SetPlayer(m_playerRef);
        m_cameraController.Initialize(m_playerRef.gameObject); 
        m_cameraController.ShouldFollow = true;
        if (m_spawners != null)
        {
            foreach (var i in m_spawners)
            {
                i.Initialize(m_hordeController, m_playerRef);
            }
        }
        m_timeEngine.StartTimer(new Timer(3, "introAnimation", OnIntroAnimationFinished));
    }
    public override void DeInit()
    {

    }
    private void OnIntroAnimationFinished()
    {
        m_levelManager.SetState(GameSessionState.DuringGame);
    }
}
