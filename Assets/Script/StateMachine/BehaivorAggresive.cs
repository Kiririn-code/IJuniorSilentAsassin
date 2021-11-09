using System.Collections;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class BehaivorAggresive : IEnemyBehaivour
{
    private Transform _target;
    private float _interest;
    private NavMeshAgent _agent;
    private Coroutine _currentInterest;
    private MonoBehaviour _monoBehaviour;

   public BehaivorAggresive(Transform target, NavMeshAgent agent,MonoBehaviour monoBehaviour)
    {
        _target = target;
        _agent = agent;
        _monoBehaviour = monoBehaviour;
    }

    public void Enter()
    {
        StartGrow(1);
        _agent.stoppingDistance = 0.8f;
        _agent.ResetPath();
    }

    public void Update()
    {
       if ( _interest== 1)
        {
            RotateToTarget();
            _agent.SetDestination(_target.position);
        }
    }

    private void RotateToTarget()
    {
        Vector3 lookVector = _target.position - _agent.transform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero)
            return;
        _agent.transform.rotation = Quaternion.RotateTowards
            (
                _agent.transform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                100 * Time.deltaTime
            );
    }
    
    private void StartGrow(float side)
    {
        if (_currentInterest != null)
            _monoBehaviour.StopCoroutine(_currentInterest);

        _currentInterest = _monoBehaviour.StartCoroutine(GrowInterest(side));
    }

    private IEnumerator GrowInterest(float value)
    {
        var time = new WaitForEndOfFrame();
        float growStep = 0.004f;

        while(_interest != value)
        {
            _interest = Mathf.MoveTowards(_interest, value, growStep);
            yield return time;
        }
    }
    
}
