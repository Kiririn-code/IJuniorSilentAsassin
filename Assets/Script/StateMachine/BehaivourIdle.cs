using UnityEngine;
using UnityEngine.AI;

public class BehaivourIdle : IEnemyBehaivour
{
    private Transform _point;
    private Vector3 _randomPoint;
    private NavMeshAgent _agent;
    private float _distanceToRandomPoint = 0.5f;
    private float _creationPointRadius = 20;

    public BehaivourIdle(NavMeshAgent agent,Transform point)
    {
        _agent = agent;
        _point = point;
    }

    public void Enter()
    {
        _randomPoint = _agent.transform.position;
        _agent.stoppingDistance = 0;
        _agent.ResetPath();
    }

    public void Update()
    {
        if (Vector3.Distance(_randomPoint,_agent.transform.position)< _distanceToRandomPoint)
        {
            if (TryGetARandomPointOnTheMap(_point.transform.position, _creationPointRadius))
                _agent.SetDestination(_randomPoint);
        }
    }

    private bool TryGetARandomPointOnTheMap(Vector3 center, float range)
    {
       Vector3 randomPoint = center + Random.insideUnitSphere * range;
       NavMeshHit hit;
        for (int i = 0; i < 10; i++)
        {
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                _randomPoint = hit.position;
                return true;
            }
        }
        return false;
    }
}
