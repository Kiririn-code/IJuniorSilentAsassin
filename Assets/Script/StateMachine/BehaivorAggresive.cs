using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using System.Threading;

[RequireComponent(typeof(NavMeshAgent))]
public class BehaivorAggresive : IEnemyBehaivour
{
    private Transform _target;
    private NavMeshAgent _agent;

   public BehaivorAggresive(Transform target, NavMeshAgent agent)
    {
        _target = target;
        _agent = agent;
    }

    public void Enter()
    {
        _agent.stoppingDistance = 0.8f;
        _agent.ResetPath();
    }

    public void Update()
    {
            RotateToTarget();
            _agent.SetDestination(_target.position);
    }

    private void RotateToTarget()
    {
        Vector3 lookVector = _target.position - _agent.transform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero)
            return;
        int rotationSpeed = 100;
        _agent.transform.rotation = Quaternion.RotateTowards
            (
                _agent.transform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                rotationSpeed * Time.deltaTime
            );
    }
}

