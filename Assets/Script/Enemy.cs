using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController))]

public class Enemy : MonoBehaviour
{
    [Range(0,90)] [SerializeField] private float _angle = 90f;
    [Range(0,10)] [SerializeField] private float _viewDistance = 10f;

    private EnemyController _controller;
    private Transform _target;

    private int _health = 100;
    private int _reward = 10;
    private float _damage = 10;
    private const float  _attackDelay = 2f;

    private float _distanceBetweenObject = 1f;

    public event UnityAction<Enemy> Died;

    public int GetRevard() => _reward;

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
        _controller = GetComponent<EnemyController>();
        _target = _controller.GetTarget();
    }

    private void Update()
    {
        float isTraget = Vector3.Distance(base.transform.position, _target.position);
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

        if(attackCooldown <= _attackDelay)
        {
            attackCooldown += Time.deltaTime;
        }
        else
        {
            player.ApplyDamage(_damage);
            attackCooldown = 0;
        }
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}
