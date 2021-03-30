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

    public event System.Action OnWaveStarted;
    public event System.Action onZombieDeath;
    void Awake()
    {
        waveTriggers = new List<WaveTrigger>();
        pathfinder = gameObject.AddComponent<NavMeshAgent2D>();
        pathfinder.isStopped = true;
        isSpawning = false;
        hasWavePassed = false;
        zombies = new List<ZombieBehaviour>();
        isWaveActive = false;
    }

    public void Initialize(HordeController hordeController, PlayerBehaviour playerBehaviour)
    {
        this.hordeController = hordeController;
        this.playerBehaviour = playerBehaviour;
        foreach (var i in waveTriggers)
        {
            i.Initialize(this);
        }
    }

    private void SpawnZombie()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-1f, 1f) + transform.position.x, Random.Range(-1f, 1f) + transform.position.y);
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
        zombies.Add(spawnedZombie);
        spawnedZombie.Initialize(GetTargetToFollow(),this);
        spawnedZombie.GetComponent<Health>().OnDeath += OnZombieDeath;
        spawnedZombie.ShouldFollow = true;
    }

    public GameObject GetTargetToFollow()
    {
        int rand = (int)Random.Range(0, 2);
        if (rand == 0)
        {
            return playerBehaviour.gameObject;
        }
        else
        {
            return hordeController.GetRandomNPC();
        }
    }

    public void StopAllZombies()
    {
        foreach (var i in zombies)
        {
            i.ShouldFollow = false;
        }
    }

    public void StopAndDestroyEverything()
    {
        isSpawning = false;
        foreach(var i in zombies)
        {
            i.Destroy();
        }
    }

    void FixedUpdate()
    {
        if (!isSpawning)
            return;
        pathfinder.destination = playerBehaviour.transform.position;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (pathfinder.remainingDistance > distanceToStartSpawning || viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
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
        if (hasWavePassed||isWaveActive)
            return;
        AudioManager.instance.PlaySound("Grawl");
        OnWaveStarted.Invoke();
        isSpawning = true;
        isWaveActive = true;
        timer = waveSpawnRate;
        timer2 = waveDurration;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        isWaveActive = false;
    }

    private void OnZombieDeath(Health health)
    {
        zombies.Remove(health.GetComponent<ZombieBehaviour>());
        onZombieDeath.Invoke();
    }


}
