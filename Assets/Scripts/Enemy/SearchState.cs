using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���܂������Ă��Ȃ����
/// </summary>
public class SearchState : IEnemyState
{
    private EnemyController _enemy;

    public SearchState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Update(float deltaTime)
    {
        _enemy.Move();

        if (_enemy.PlayerSearch())
        {
            //�v���C���[��ڎ����Ă�����SearchState�ɕύX����
            _enemy.StateMachine.TransitionState(new AttackState(_enemy));
        }
    }

    public void Exit() 
    {
        
    }
}
