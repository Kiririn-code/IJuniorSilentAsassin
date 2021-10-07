using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLine : MonoBehaviour
{
    [SerializeField] private float _viewAngle = 50f;
    [SerializeField] private float _viewDistance = 10;
    void Update()
    {
        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _viewDistance);
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, _viewAngle / 2f, 0)) * (transform.forward * _viewDistance);
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }
}
