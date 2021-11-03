using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    private Transform _generateCenter;

    private Dictionary<Type,IEnemyBehaivour> _enemyBehaivors;
    private IEnemyBehaivour _currentBehaivor;

    public event UnityAction BehaivourChanged;

    public void InitEnemy(Transform target, Transform center)
    {
        _target = target;
        _generateCenter = center;

        _agent = GetComponent<NavMeshAgent>();

        InitDictionary();
        SetBehaivourByDefault();

    }

    private void Update()
    {
        if (_currentBehaivor != null)
                _currentBehaivor.Update();
    }

    private void SetBehaivourByDefault()
    {
        SetIdleBehaivour();
    }

    private void InitDictionary()
    {
        _enemyBehaivors = new Dictionary<Type, IEnemyBehaivour>
        {
            [typeof(BehaivorAggresive)] = new BehaivorAggresive(_target, _agent, this),
            [typeof(BehaivourIdle)] = new BehaivourIdle(_agent, _generateCenter)
        };
    }

    private void SetBehaivour(IEnemyBehaivour newBehaivour)
    {
        if(_currentBehaivor != null)
                _currentBehaivor.Exit();

        _currentBehaivor = newBehaivour;

        _currentBehaivor.Enter();
    }

    private IEnemyBehaivour GetBehaivour<T>() where T : IEnemyBehaivour
    {
        return _enemyBehaivors[typeof(T)];
    }
    public void SetAggresiveBehaivour()
    {
        var behaivour = GetBehaivour<BehaivorAggresive>();
        SetBehaivour(behaivour);
    }

    public void SetIdleBehaivour()
    {
        BehaivourChanged?.Invoke();
        var behaivour = GetBehaivour<BehaivourIdle>();
        SetBehaivour(behaivour);
    }

    public Type GetBehaivourType() => _currentBehaivor.GetType();
    public float GetMagnitude() => _agent.velocity.magnitude;
    public Transform GetTarget() => _target;
}
