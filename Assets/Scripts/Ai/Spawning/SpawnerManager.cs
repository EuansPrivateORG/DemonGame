using DemonInfo;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(WaveManager))]
[RequireComponent(typeof(DemonSpawner))]
[RequireComponent(typeof(DemonPoolers))]
[RequireComponent(typeof(Spawners))]
public class SpawnerManager : MonoBehaviour
{
    [HideInInspector] public WaveManager WaveManager;
    [HideInInspector] public DemonSpawner DemonSpawner;

    [Header("Player")]
    public Transform player;
    [SerializeField] NavMeshAgent playerAgent;

    [Header("Spawning Stats")]
    public bool canSpawn;
    [SerializeField] int maxDemonsAtOnce;
    public float timeBetweenSpawns;
    [SerializeField] float timeBetweenRounds;
    public int demonsToSpawnEachTick;
    public Vector2Int minMax;

    [Header("Animation Curves")]
    public AnimationCurve demonsToSpawn;
    public AnimationCurve spawnsEachTick;

    [Header("Display Stats")]
    [Range(0, 10000)] public int currentRound;
    public int maxDemonsToSpawn;
    public int currentDemons;
    public bool EndOfRound;
    public bool StartOfRound;

    [Header("Timers")]
    private float spawnTimer;
    private float endRoundTimer;

    private void Awake()
    {
        WaveManager = GetComponent<WaveManager>();
        DemonSpawner = GetComponent<DemonSpawner>();
    }
    private void Start()
    {
        WaveStart();

        DemonSpawner.ActiveSpawners(player, playerAgent, this);
    }

    private void Update()
    {
        Timers();
        Bools();

        if(EndOfRound == true)
        {
            WaveEnd();
            EndOfRound = false;
            StartOfRound = true;
        }

        if(StartOfRound == true)
        {
            endRoundTimer += Time.deltaTime;
            if (HelperFuntions.TimerGreaterThan(endRoundTimer, timeBetweenRounds))
            {
                WaveStart();
                endRoundTimer = 0f;
                StartOfRound = false;
            }
        }

        if (HelperFuntions.TimerGreaterThan(spawnTimer, timeBetweenSpawns) && canSpawn == true)
        {
            if (HelperFuntions.IntGreaterThanOrEqual(maxDemonsAtOnce, currentDemons))
            {
                spawnTimer = 0;

                if (DemonSpawner.DemonCount <= 0) // if no demons to spawn return
                {
                    return;
                }

                int toSpawn = maxDemonsAtOnce - currentDemons;

                if (toSpawn <= demonsToSpawnEachTick) { }
                else { toSpawn = demonsToSpawnEachTick; }

                if (maxDemonsToSpawn < toSpawn) { toSpawn = maxDemonsToSpawn; }

                if (toSpawn > 0)
                {
                    DemonSpawner.ActiveSpawners(player, playerAgent, this); // if demoms to spawn check spawners

                    for (int i = 0; i < toSpawn; i++)
                    {
                        if (DemonSpawner.SpawnDemon(this)) // if a demon can be spawned, if requested spawner can spawn return true 
                        {
                            
                        }
                    }
                }
            }
        }
    }

    void WaveStart()
    {
        WaveManager.NextWave(currentRound);

        maxDemonsToSpawn = MaxDemonsToSpawn;
        demonsToSpawnEachTick = DemonSpawnsEachTick;

        DemonSpawner.DemonQueue = WaveManager.GetDemonToSpawn(maxDemonsToSpawn);

        currentRound++;
    }

    void WaveEnd()
    {
        DemonSpawner.DemonQueue.Clear();
    }

    public void DemonKilled()
    {
        currentDemons--;
    }

    void Timers()
    {
        spawnTimer += Time.deltaTime;
    }

    void Bools()
    {
        if (maxDemonsToSpawn <= 0 && currentDemons <= 0 && StartOfRound == false) EndOfRound = true;
    }

    public int MaxDemonsToSpawn
    {
        get { return HelperFuntions.EvaluateAnimationCuveInt(demonsToSpawn, currentRound); }
    }

    public int DemonSpawnsEachTick
    {
        get { return HelperFuntions.EvaluateAnimationCuveInt(spawnsEachTick, currentRound); }
    }
}
