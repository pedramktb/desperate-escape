using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private PlayerData startPlayerData;
    private HordeData startHordeData;
    private PlayerData playerDataToLoad;
    private HordeData hordeDataToLoad;
    private bool ShowTutorial = true;
    public int levelCount;
    public int currentLevel;

    private void Start()
    {
        levelCount = SceneManager.sceneCountInBuildSettings;
        currentLevel = 0;
    }

    public void Play()
    {
        InitializeGameData();
        playerDataToLoad = startPlayerData;
        hordeDataToLoad = startHordeData;
        StartLevel(1);
    }

    private void StartLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        var levelManager = FindObjectOfType<LevelManager>();
        levelManager.Initialize(playerDataToLoad, hordeDataToLoad, ref ShowTutorial, this);
    }

    public void LoadNextLevel(PlayerBehaviour playerBehaviour, HordeController hordeController)
    {
        PlayerData playerData = playerBehaviour.GetData();
        playerData.Kill = playerBehaviour.currentKills;
        playerData.Score += hordeController.GetTeamValue();
        playerDataToLoad = playerData;


        List<NPCData> horde = new List<NPCData>();
        foreach (var i in hordeController.GetNPCs())
        {
            horde.Add(i.GetData());
        }
        hordeDataToLoad = new HordeData(horde);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartLevel(nextSceneIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void InitializeGameData()
    {
        startPlayerData = new PlayerData(5, 10, 5, 5, 30, 300, 10, 3);
        List<NPCData> horde = new List<NPCData>();
        horde.Add(new DoctorData(10, 5, 3, 2, 5));
        horde.Add(new NormalGuyData(10, 5, 3));
        horde.Add(new ShieldGuyData(10, 5, 3, 3));
        horde.Add(new AmmoGuyData(10, 5, 3, 10, 100, 3));
        startHordeData = new HordeData(horde);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
