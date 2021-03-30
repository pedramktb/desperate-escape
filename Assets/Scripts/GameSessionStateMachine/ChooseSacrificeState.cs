using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.StateMachine;
using Utility;
public class ChooseSacrificeState : State
{
    GameManager m_gameManager;
    LevelManager m_levelManager;
    UIManager m_UIManager;
    TimeEngine m_timeEngine;
    PlayerBehaviour m_playerRef;
    HordeController m_hordeController;
    public ChooseSacrificeState(LevelManager levelManager, UIManager UIManager, TimeEngine timeEngine, GameManager gameManager, PlayerBehaviour playerRef, HordeController hordeController)
    {
        m_levelManager = levelManager;
        m_UIManager = UIManager;
        m_timeEngine = timeEngine;
        m_gameManager = gameManager;
        m_playerRef = playerRef;
        m_hordeController = hordeController;
    }
    public override void Init()
    {
        m_UIManager.ShowRoundPanel();
        m_timeEngine.StartTimer(new Timer(3, "round finished hiding", Done));
    }
    public override void DeInit()
    {
        m_UIManager.HideRoundPanel();
    }

    public void Done()
    {
        m_UIManager.HideRoundPanel();
        if (m_gameManager.currentLevel + 1 == m_gameManager.levelCount)
        {
            m_levelManager.SetState(GameSessionState.Won);
        }
        else
        {
            m_gameManager.LoadNextLevel(m_playerRef, m_hordeController);
        }
    }
}
