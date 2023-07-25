using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BasicDemon : DemonBase
{
    public override void Setup()
    {
        
    }
    public override void Tick()
    {
        PathFinding();
    }
    public override void Attack()
    {
        // target within range attack
    }

    public override void PathFinding()
    {
        if (_calculatePath == true)
        {
            Transform pathingTarget = _target.transform;

            _currentPath = CalculatePath(pathingTarget);

            _agent.SetPath(_currentPath);

            _calculatePath = false;
        }
    }
}
