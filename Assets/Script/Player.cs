using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 5;

    private float _health = 100;
    private int _score;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up, 200 * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * _speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ApplyDamage(100);
        }
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public void RestartPlayer()
    {
        _score = 0;
        transform.position = _startPosition;
    }
}
