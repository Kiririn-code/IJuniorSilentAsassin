using System.Collections;
using UnityEngine;
[RequireComponent(typeof(EmemyController))]

public class Enemy : MonoBehaviour
{
    private EmemyController _controller;

    [SerializeField] private Transform _target;

    private float _damage = 10;

    private float _distanceBetweenObject = 2f;
    private float _viewDistance = 10f;
    private float _angle = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            DoAttack(player);
            Debug.Log("Hit");
        }
    }

    private void Start()
    {
        _controller = GetComponent<EmemyController>();
    }

    private void Update()
    {
        float isTraget = Vector3.Distance(transform.position, _target.position);
        if ((isTraget <= _distanceBetweenObject) || IsWiew())
        {
            if (_controller.GetBehaivourType() != typeof(BehaivorAggresive))
            {
                _controller.SetAggresiveBehaivour();
            }
        }
        else
        {
            if(_controller.GetBehaivourType() != typeof(BehaivourIdle))
            {
                _controller.SetIdleBehaivour();
            }
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

    private void DoAttack(Player player)
    {
        float attackCooldown =0;

        if(attackCooldown <=3)
        {
            attackCooldown += Time.deltaTime;
        }
        else
        {
            player.GetDamage(_damage);
            attackCooldown = 0;
        }
    }
}
