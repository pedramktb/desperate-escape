using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] MainUI mainUIRef;
    [SerializeField] GameObject wavePanelRef;
    [SerializeField] GameObject startingPanelRef;
    [SerializeField] GameObject losePanelRef;
    [SerializeField] GameObject tutorialPanelRef;
    [SerializeField] GameObject wonPanelRef;
    [SerializeField] GameObject roundFinishedPanelRef;
    GameManager gameManager;
    PlayerBehaviour playerRef;
    HordeController hordeRef;

    public Action OnTutorialComplete;
    public void ShowTutorialPanel()
    {
        tutorialPanelRef.SetActive(true);
    }

    public void TutorialDone()
    {
        tutorialPanelRef.SetActive(false);
        OnTutorialComplete.Invoke();
    }

    public void Initialize(PlayerBehaviour playerRef, HordeController hordeRef , GameManager gameManager)
    {
        this.playerRef = playerRef;
        this.hordeRef = hordeRef;
        this.gameManager = gameManager;
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
    public void ShowLosePanel()
    {
        losePanelRef.SetActive(true);
    }

    public void HideLosePanel()
    {
        losePanelRef.SetActive(false);
    }

    public void Restart()
    {
        gameManager.Restart();
    }

    public void ShowWonPanel()
    {
        wonPanelRef.SetActive(true);
    }

    public void HideWonPanel()
    {
        wonPanelRef.SetActive(false);
    }
    public void ShowRoundPanel()
    {
        roundFinishedPanelRef.SetActive(true);
    }

    public void HideRoundPanel()
    {
        roundFinishedPanelRef.SetActive(false);
    }
}
