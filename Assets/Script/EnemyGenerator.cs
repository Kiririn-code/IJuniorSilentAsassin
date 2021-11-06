using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Player _target;
    [SerializeField] private Transform _center;

    private List<Enemy> _enemies;
    private float _gapBetweenSpawn;
    private const float _spawnDelay = 3f;
    private const int _maxEnemiesOnMap = 3;

    private void Start()
    {
        _enemies = new List<Enemy>();
    }

    private void Update()
    {
        if (_gapBetweenSpawn >= _spawnDelay && _enemies.Count <= _maxEnemiesOnMap - 1)
        {
            Spawn();
            _gapBetweenSpawn = 0;
        }
        _gapBetweenSpawn += Time.deltaTime;
    }

    public void RestartSpawn()
    {
        if (_enemies.Count >= 0)
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }
            _enemies = new List<Enemy>();
        }
    }

    private void Spawn()
    {
        EnemyController enemyController = Instantiate(_enemyPrefab, _center.position, Quaternion.identity, transform).GetComponent<EnemyController>();
        Enemy enemy = enemyController.gameObject.GetComponent<Enemy>();

        enemy.Died += OnEnemyDyind;
        _enemies.Add(enemy);

        enemyController.InitEnemy(_target.transform, _center);
    }

    private void OnEnemyDyind(Enemy enemy)
    {
        _target.AddScore(enemy.GetRevard());
        _enemies.Remove(enemy);
        enemy.Died -= OnEnemyDyind;
    }
}
