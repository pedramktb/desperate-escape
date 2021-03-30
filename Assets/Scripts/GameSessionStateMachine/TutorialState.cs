using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityCore.StateMachine;

public class TutorialState : State
{

    LevelManager m_levelManager;
    UIManager m_UIManager;
    public TutorialState(LevelManager levelManager, UIManager UIManager)
    {
        m_levelManager = levelManager;
        m_UIManager = UIManager;
        
    }
    public override void Init()
    {
        m_UIManager.OnTutorialComplete += OnTutorialComplete; 
        m_UIManager.ShowTutorialPanel();
    }
    public override void DeInit()
    {
        m_UIManager.OnTutorialComplete -= OnTutorialComplete;
    }

    private void OnTutorialComplete()
    {
        m_levelManager.SetState(GameSessionState.Intro);
    }
}
