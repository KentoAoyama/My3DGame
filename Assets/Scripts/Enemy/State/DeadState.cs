using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���S���Ă�����
/// </summary>
public class DeadState : IEnemyState
{
    EnemyController _enemy;

    public DeadState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Update(float deltaTime)
    {
        
    }

    public void Exit() 
    {
        
    }
}
