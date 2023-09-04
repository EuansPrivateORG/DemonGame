using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BasicDemon : DemonBase
{
    [Header("Demon Pathing")]
    [SerializeField] float distanceToRespawn;

    [Header("Demon Health Algorithm")]
    [SerializeField] int m_xAmountOfRounds;
    [SerializeField] float m_HealthToAdd;
    [SerializeField] float m_HealthMultiplier;

    [Header("ObstacleDetection")]
    [SerializeField] DestroyObstacle m_obstacle;

    public override void OnAwakened()
    {
        m_obstacle = GetComponent<DestroyObstacle>();
    }
    public override void Setup()
    {
        UpdateHealthToCurrentRound(_spawnerManager.currentRound);
        base.Setup();
    }

    public override void Tick()
    {
        PathFinding(_agent.enabled);
        DetectPlayer(_agent.enabled);

        m_obstacle.Detection();

        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }
    public override void OnAttack()
    {
        base.OnAttack();

        // deal damage
        if(Vector3.Distance(_target.position,transform.position) < _attackRange)
            _target.GetComponent<Health>().TakeDmg(_damage);
    }
    public override void OnSpawn(Transform target, bool defaultSpawn = true)
    {
        if(defaultSpawn == true) { ritualSpawn = false; }
        else { ritualSpawn = true; }

        base.OnSpawn(target);
        UpdateHealthToCurrentRound(_spawnerManager.currentRound);
        CalculateAndSetPath(target);
        SetHealth(_health.maxHealth);
        _health.dead = false;
    }
    public override void OnRespawn(bool defaultDespawn = true)
    {
        base.OnRespawn(defaultDespawn);

        
    }
    public override void OnDeath() // add back to pool of demon type
    {
        base.OnDeath();
        
        if(ritualSpawn == true)
        {
            _spawnerManager.currentRitual.currentDemons--;
            _spawnerManager.currentRitual.demonsLeft--;
        }
    }
    public override void OnBuff()
    {
        // apply stat updates
    }
    public override void OnHit()
    {
        base.OnHit();
        // do hit stuff
    }

    public override void PathFinding(bool active)
    {
        if(active == true)
        {
            CalculateAndSetPath(_target);
        }
    }

    public override void DetectPlayer(bool active)
    {
        if(active == true)
        {
            float dist = DistanceToTargetUnits;

            if (dist < _attackRange)
            {
                PlayAnimation("Attack");
            }

            dist = DistanceToTargetNavmesh;

            if (dist > 100000) dist = 0;

            if(dist > distanceToRespawn)
            {
                OnRespawn();
            }
        }
    }
    public override void CalculateStats(int round)
    {
        if (round <= m_xAmountOfRounds)
        {
            _health.maxHealth += m_HealthToAdd;
        }
        else { _health.maxHealth = _health.maxHealth * m_HealthMultiplier; }

        _moveSpeed = _moveSpeedCurve.Evaluate(round);
    }

    public override void UpdateHealthToCurrentRound(int currentRound)
    {
        if(currentRound != _currentUpdatedRound)
        {
            int num = currentRound - _currentUpdatedRound;

            for (int round = 0 + _currentUpdatedRound; round < num + _currentUpdatedRound; round++)
            {
                CalculateStats(round);
            }

            _currentUpdatedRound = currentRound;
        }
    }

    private void OnDestroy()
    {
        _health.OnDeath -= OnDeath;
        _health.OnHit -= OnHit;
    }
}
