using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;

    private Dictionary<Type,IEnemyBehaivour> _enemyBehaivors;
    private IEnemyBehaivour _currentBehaivor;

    private void Awake()
    {
        InitDictionary();
        SetBehaivourByDefault();
    }

    private void Update()
    {
        if (_currentBehaivor != null)
        {
            _currentBehaivor.Update();
        }
    }

    private void SetBehaivourByDefault()
    {
        SetIdleBehaivour();
    }

    private void InitDictionary()
    {
        _enemyBehaivors = new Dictionary<Type, IEnemyBehaivour>
        {
            [typeof(BehaivorAggresive)] = new BehaivorAggresive(_target,_agent,this),
            [typeof(BehaivourIdle)] = new BehaivourIdle()
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
        var behaivour = GetBehaivour<BehaivourIdle>();
        SetBehaivour(behaivour);
    }

    public Type GetBehaivourType() => _currentBehaivor.GetType();
}
