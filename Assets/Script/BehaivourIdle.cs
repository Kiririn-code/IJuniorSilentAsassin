using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaivourIdle : IEnemyBehaivour
{
    public void Enter()
    {
        Debug.Log("Idle Enter");

    }

    public void Exit()
    {
        Debug.Log("Idle Exit");
    }

    public void Update()
    {
        Debug.Log("Idle Update");
    }
}
