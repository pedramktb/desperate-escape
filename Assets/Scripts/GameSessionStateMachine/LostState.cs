using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.StateMachine;

public class LostState : State
{
    LevelManager m_levelManager;
    UIManager m_UIManager;
    public LostState(LevelManager levelManager,UIManager UIManager)
    {
        m_levelManager = levelManager;
        m_UIManager = UIManager;
    }
    public override void Init()
    {
        m_UIManager.ShowLosePanel();
    }
    public override void DeInit()
    {
        m_UIManager.HideLosePanel();
    }
}
