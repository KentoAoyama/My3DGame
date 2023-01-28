using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�������čU�����s�����
/// </summary>
public class AttackState : IEnemyState
{
    EnemyController _enemy;

    public AttackState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Update(float deltaTime)
    {
        _enemy.Move();
    }

    public void Exit() 
    {
        
    }
}
