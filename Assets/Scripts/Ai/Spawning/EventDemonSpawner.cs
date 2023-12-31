using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDemonSpawner : MonoBehaviour
{
    [SerializeField] Wave wave;

    public bool spawn = false;

    public int demonsToSpawn;
    public int MaxDemonsAtOnce;
    public int SpawnsPerTick;
    public int TimeBetweenSpawns;

    Queue<DemonType> demonQueue;

    private void Initalise()
    {
        demonQueue = DemonWave;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private Queue<DemonType> DemonWave
    {
        get
        {
            if (demonQueue != null)
            {
                Debug.Log("Wasnt null return value");
                return demonQueue;
            }

            Debug.Log("Was null returning wave");
            return demonQueue = Wave.GetWave(demonsToSpawn, wave);
        }

        set
        {
            DemonWave = value;
        }
    }
}
