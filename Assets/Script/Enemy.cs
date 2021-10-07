using System.Collections;
using UnityEngine;
[RequireComponent(typeof(EmemyController))]

public class Enemy : MonoBehaviour
{
    private EmemyController _controller;

    [SerializeField] private Transform _target;

    private float _distanceBetweenObject = 2f;
    private float _viewDistance = 10f;
    private float _angle = 50f;

    private void Start()
    {
        _controller = GetComponent<EmemyController>();
    }

    void Update()
    {
        float isTraget = Vector3.Distance(transform.position, _target.position);
        if ((isTraget <= _distanceBetweenObject) || IsWiew())
        {
            if(_controller.GetBehaivourType() != typeof(BehaivorAggresive))
                _controller.SetAggresiveBehaivour();
        }
    }

    private bool IsWiew()
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
}
