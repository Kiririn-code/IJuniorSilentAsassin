using UnityEngine;
using UnityEngine.AI;

public class BehaivourIdle : IEnemyBehaivour
{
    private Transform _point;
    private Vector3 _randomPoint;
    private NavMeshAgent _agent;

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

    [System.Obsolete]
    public void Exit()
    {
        _agent.Stop();
    }

    public void Update()
    {
        if (Vector3.Distance(_randomPoint,_agent.transform.position)< 0.5f)
        {
            if (RandomPoint(_point.transform.position, 20))
                _agent.SetDestination(_randomPoint);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Agent " + _agent.transform.position);
            Debug.Log("random " + _randomPoint);
            Debug.Log("Round Agent " + Mathf.Round(_agent.transform.position.x));
            Debug.Log("Rount Random " + Mathf.Round(_randomPoint.x));
            Debug.Log("Distance" + Vector3.Distance(_randomPoint, _agent.transform.position));
        }
    }

    private bool RandomPoint(Vector3 center, float range)
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

        //_randomPoint = _point.position;
        return false;
    }
}