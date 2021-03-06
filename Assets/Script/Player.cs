using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage = 50;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Animator _animator;
    
    private float _attackRadius = 0.35f;
    private float _health;
    private int _score;
    private Vector3 _startPosition;

    private const string Walk = "IsWalk";
    private const string Attack = "Attack";
    private const string Hit = "GetHit";

    public event UnityAction<float> HealthChanged;
    public event UnityAction<int> ScoreChahged;
    public event UnityAction Died;

    private void Start()
    {
        _startPosition = transform.position;
        _health = 100;
        HealthChanged?.Invoke(_health);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _animator.SetBool(Walk,true);
        }
        else
        {
            _animator.SetBool(Walk, false);
        }
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up, 200 * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * _speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Assault();
            _animator.SetTrigger(Attack);
        }

    }

    private void Assault()
    {
        Collider[] enemies = Physics.OverlapSphere(_attackPoint.position, _attackRadius, _mask);

        foreach (var enemy in enemies)
        {
            enemy.gameObject.GetComponent<Enemy>().ApplyDamage(_damage);
        }
    }

    public void ApplyDamage(float damage)
    {
        _animator.SetTrigger(Hit);
        _health -= damage;
        HealthChanged?.Invoke(_health);
        if (_health <= 0)
         Death();
    }

    private void Death()
    {
        Died?.Invoke();
    }

    public void AddScore(int score)
    {
        _score += score;
        ScoreChahged?.Invoke(_score);
    }

    public void RestartPlayer()
    {
        _score = 0;
        _health = 100;
        transform.position = _startPosition;
        ScoreChahged.Invoke(_score);
        HealthChanged.Invoke(_health);
    }
}
