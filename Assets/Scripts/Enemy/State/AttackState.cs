using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�������čU�����s�����
/// </summary>
public class AttackState : IEnemyState
{
    private EnemyController _enemy;

    public AttackState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.ChangeIKWeight(1f);
    }

    public void Update(float deltaTime)
    {
        _enemy.Move(deltaTime);
        _enemy.Attack(deltaTime);

        if (!_enemy.PlayerSearch())
        {
            //�v���C���[��ڎ����Ă��Ȃ����SearchState�ɕύX����
            _enemy.StateMachine.TransitionState(new SearchState(_enemy));
        }
    }

    public void Exit() 
    {
        _enemy.ChangeIKWeight(0f);
        _enemy.AttackStop();
    }
}
