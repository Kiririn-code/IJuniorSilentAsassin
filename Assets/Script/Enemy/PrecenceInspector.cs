using UnityEngine;

public class PrecenceInspector : MonoBehaviour
{
    [Range(0, 50)] [SerializeField] private float _angle = 90f;
    [Range(0, 5)] [SerializeField] private float _viewDistance = 10f;
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public bool IsWiew()
    {
        float realAngile = Vector3.Angle(transform.forward, _target.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, _target.position - transform.position, out hit, _viewDistance))
        {
            if (realAngile < _angle / 2f && Vector3.Distance(transform.position, _target.position) <= _viewDistance && hit.transform == _target.transform)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, _angle / 2f, 0)) * (transform.forward * _viewDistance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, _angle / 2f, 0)) * (transform.forward * _viewDistance);
        Gizmos.DrawLine(transform.position, left);
        Gizmos.DrawLine(transform.position, right);
    }
}
