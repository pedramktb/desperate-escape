using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] MainUI mainUIRef;
    [SerializeField] GameObject wavePanelRef;
    [SerializeField] GameObject startingPanelRef;
    PlayerBehaviour playerRef;
    HordeController hordeRef;

    public Action OnTutorialComplete;
    public void ShowTutorialPanel()
    {

    }

    public void TutorialDone()
    {
        OnTutorialComplete.Invoke();
    }

    public void Initialize(PlayerBehaviour playerRef, HordeController hordeRef)
    {
        this.playerRef = playerRef;
        this.hordeRef = hordeRef;
        mainUIRef.Initialize(playerRef, hordeRef);
    }

    public void ShowWavePanel()
    {
        wavePanelRef.SetActive(true);
    }

    public void HideWavePanel()
    {
        wavePanelRef.SetActive(false);

    }
    public void ShowMainUI()
    {
        mainUIRef.gameObject.SetActive(true);
    }

    public void HideMainUI()
    {
        mainUIRef.gameObject.SetActive(false);

    }
    public void ShowStartingPanel()
    {
        startingPanelRef.SetActive(true);
    }

    public void HideStartingPanel()
    {
        startingPanelRef.SetActive(false);
    }
}
