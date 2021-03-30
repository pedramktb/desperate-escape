using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Action<NPCBehaviour> OnNPCDeath;
    private PlayerData startPlayerData;
    private HordeData startHordeData;
    private PlayerData playerDataToLoad;
    private HordeData hordeDataToLoad;
    private bool ShowTutorial = true;

    public void Start()
    {
        
    }

    public void Play()
    {
        InitializeGameData();
        StartLevel(1, startPlayerData, startHordeData);
    }

    private void StartLevel(int levelIndex, PlayerData playerData, HordeData hordeData)
    {
        playerDataToLoad = playerData;
        hordeDataToLoad = hordeData;
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var levelManager = FindObjectOfType<LevelManager>();
        levelManager.Initialize(playerDataToLoad, hordeDataToLoad,ref ShowTutorial,this);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCount > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
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
