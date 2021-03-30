using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.GameSystems;
public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] ZombieType type;
    [SerializeField] int spawnRate;
    [SerializeField] int waveDurration;
    [SerializeField] int waveSpawnRate;
    [SerializeField] float distanceToStartSpawning;
    [SerializeField] GameObject zombieNormalPrefabRef;
    [SerializeField] GameObject zombieFastPrefabRef;
    [SerializeField] GameObject zombieStrongPrefabRef;
    List<ZombieBehaviour> zombies;
    float timer;
    float timer2;

    NavMeshAgent2D pathfinder;
    HordeController hordeController;
    PlayerBehaviour playerBehaviour;
    bool isSpawning;
    bool hasWavePassed;
    bool isWaveActive;
    List<WaveTrigger> waveTriggers;

    void Awake()
    {
        waveTriggers = new List<WaveTrigger>();
        pathfinder = gameObject.AddComponent<NavMeshAgent2D>();
        pathfinder.isStopped = true;
        isSpawning = false;
        hasWavePassed = false;
        isWaveActive = false;
    }

    public void Initialize(HordeController hordeController, PlayerBehaviour playerBehaviour)
    {
        this.hordeController = hordeController;
        this.playerBehaviour = playerBehaviour;
        foreach(var i in waveTriggers){
            i.Initialize(this);
        }
    }

    private void SpawnZombie()
    {
        Vector2 spawnPos = (Vector2)transform.position + new Vector2(Random.Range(-1, 1) + transform.position.x, Random.Range(-1, 1) + transform.position.y);
        ZombieBehaviour spawnedZombie;
        if (type == ZombieType.Normal)
        {
            spawnedZombie = Instantiate(zombieNormalPrefabRef, spawnPos, Quaternion.identity, transform).GetComponent<ZombieBehaviour>();
        }
        else if (type == ZombieType.Fast)
        {
            spawnedZombie = Instantiate(zombieFastPrefabRef, spawnPos, Quaternion.identity, transform).GetComponent<ZombieBehaviour>();
        }
        else
        {
            spawnedZombie = Instantiate(zombieStrongPrefabRef, spawnPos, Quaternion.identity, transform).GetComponent<ZombieBehaviour>();
        }
        int rand = (int)Random.Range(0, 2);
        if (rand == 0)
        {
            spawnedZombie.Initialize(playerBehaviour.gameObject);
        }
        else
        {
            spawnedZombie.Initialize(hordeController.GetRandomNPC());
        }
        spawnedZombie.GetComponent<Health>().OnDeath += OnZombieDeath;
        spawnedZombie.ShouldFollow = true;
    }

    public void StopAllZombies()
    {
        foreach (var i in zombies)
        {
            i.ShouldFollow = false;
        }
    }

    void FixedUpdate()
    {
        if (!isSpawning)
            return;
        pathfinder.destination = playerBehaviour.transform.position;
        if(pathfinder.remainingDistance > distanceToStartSpawning){
            return;
        }
        if (isWaveActive)
        {
            timer2 -= Time.fixedDeltaTime;
            if (timer2 < 0)
            {
                hasWavePassed = true;
                isWaveActive = false;
                timer = spawnRate;
            }
            else
            {
                timer -= Time.fixedDeltaTime;
                if (timer < 0)
                {
                    SpawnZombie();
                    timer = waveSpawnRate;
                }
            }
        }
        else
        {
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                SpawnZombie();
                timer = spawnRate;
            }
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timer = spawnRate;
    }

    public void StartWave()
    {
        if(hasWavePassed)
            return;
        isSpawning = true;
        isWaveActive = true;
        timer = waveSpawnRate;
        timer2 = waveDurration;
    }

    public void StopSpawning(){
        isSpawning = false;
        isWaveActive = false;
    }

    private void OnZombieDeath(Health health)
    {
        zombies.Remove(health.GetComponent<ZombieBehaviour>());
    }


}
