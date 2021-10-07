using System.Collections;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class BehaivorAggresive : MonoBehaviour, IEnemyBehaivour
{
    [SerializeField] private Transform _target;

    private float _interest;
    private NavMeshAgent _agent;
    private Coroutine _currentInterest;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    public void Enter()
    {
        StartGrow(1);
    }

    public void Exit()
    {
        StartGrow(0);
    }

    public void Update()
    {
        if ( _interest== 1)
        {
            RotateToTarget();
            GoToTarget();
        }
        Debug.Log("UpdateAGRR");
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
                5 * Time.deltaTime
            );
    }
    private void GoToTarget()
    {
        _agent.SetDestination(_target.position);
    }

    private void StartGrow(float side)
    {
        if (_currentInterest != null)
            StopCoroutine(_currentInterest);

        _currentInterest = StartCoroutine(GrowInterest(side));
    }

    private IEnumerator GrowInterest(float value)
    {
        var time = new WaitForEndOfFrame();

        while(_interest != value)
        {
            _interest = Mathf.MoveTowards(_interest, value, 0.1f);
            yield return time;
        }
    }
}
