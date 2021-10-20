using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private int _speed = 5;

    private float _health = 100;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
    }
}
