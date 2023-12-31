using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemonInfo;

[Serializable]
public class DemonType
{
    public DemonID Id;
    public SpawnerType SpawnerType;
    [HideInInspector] public SpeedType SpeedType;
    [HideInInspector] public SpawnType SpawnType;
}