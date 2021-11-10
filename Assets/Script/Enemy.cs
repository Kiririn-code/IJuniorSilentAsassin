using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _boom;
    [SerializeField] private Animator _animator;
    [Range(0,50)] [SerializeField] private float _angle = 90f;
    [Range(0,5)] [SerializeField] private float _viewDistance = 10f;

    private EnemyController _controller;
    private Transform _target;

    private int _health = 100;
    private int _reward = 10;
    private float _damage = 10;

    private float _distanceBetweenObject = 1f;

    public event UnityAction<Enemy> Died;

    public int GetRevard() => _reward;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.ApplyDamage(_damage);
            _animator.SetTrigger("Attack");
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

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _controller.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        _animator.SetTrigger("Die");
        StartCoroutine(DestroyEnemyy());
        Died?.Invoke(this);
    }

    private IEnumerator DestroyEnemyy()
    {
        var time = new WaitForSeconds(2);
        yield return time;
        Instantiate(_boom, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, _angle / 2f, 0)) * (transform.forward * _viewDistance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, _angle / 2f, 0)) * (transform.forward * _viewDistance);
        Gizmos.DrawLine(transform.position, left);
        Gizmos.DrawLine(transform.position, right);
    }
}
