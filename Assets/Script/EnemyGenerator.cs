using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _center;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            var enemy = Instantiate(_enemyPrefab, _center.position,Quaternion.identity,transform).GetComponent<EmemyController>();
            enemy.InitEnemy(_target, _center);
        }
    }
}
