using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BasicDemon : DemonBase
{

    public override void Setup()
    {
        _health.OnDeath += OnDeath;
        _health.OnHit += OnHit;
        _agent.stoppingDistance = _stoppingDistance;
    }
    public override void Tick()
    {
        PathFinding();
    }
    public override void OnAttack()
    {
        // deal damage
    }
    public override void OnSpawn(Transform target)
    {
        CalculateStats(_spawner.currentRound);
        CalculateAndSetPath(target);

        _health.health = _maxHealth;
    }
    public override void OnDeath() // add back to pool of demon type
    {
        _pooledObject.Despawn();
        _spawner.DemonKilled();
    }
    public override void OnBuff()
    {
        // apply stat updates
    }
    public override void OnHit()
    {
        // do hit stuff
    }

    public override void PathFinding()
    {
        CalculateAndSetPath(_target);
    }
}
