using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PrecenceInspector))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _boom;
    [SerializeField] private int _health = 100;
    [SerializeField] private float _damage = 10;

    private Animator _animator;
    private PrecenceInspector _precenceView;
    private Transform _target;
    private EnemyController _controller;
    private float _distanceBetweenObject = 1f;

    private const string _attack = "Attack";
    private const string _die = "Die";

    public int Reward { get; private set; } = 10;
    public event UnityAction<Enemy> Died;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.ApplyDamage(_damage);
            _animator.SetTrigger(_attack);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _precenceView = GetComponent<PrecenceInspector>();
        _controller = GetComponent<EnemyController>();
        _target = _controller.GetTarget();
        _precenceView.SetTarget(_target);
    }

    private void Update()
    {
        float isTraget = Vector3.Distance(transform.position, _target.position);
        if ((isTraget <= _distanceBetweenObject) || _precenceView.IsWiew())
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
        _animator.SetTrigger(_die);
        StartCoroutine(DestroyEnemy());
        Died?.Invoke(this);
    }

    private IEnumerator DestroyEnemy()
    {
        var time = new WaitForSeconds(2);
        yield return time;
        Instantiate(_boom, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
