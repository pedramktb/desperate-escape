using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.StateMachine;

public class WonState : State
{
    LevelManager m_levelManager;
    UIManager m_UIManager;
    public WonState(LevelManager levelManager, UIManager UIManager)
    {
        m_levelManager = levelManager;
        m_UIManager = UIManager;
    }
    public override void Init()
    {
        throw new System.NotImplementedException();
    }
    public override void DeInit()
    {
        throw new System.NotImplementedException();
    }
}