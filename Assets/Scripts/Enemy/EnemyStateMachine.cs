using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateMachine
{
    private IEnemyState _currentState;
    public IEnemyState CurrentState => _currentState;

    //State�̃N���X
    private SearchState _search;
    public SearchState Search => _search;

    private AttackState _attack;
    public AttackState Attack => _attack;

    private DeadState _dead;
    public DeadState Dead => _dead;

    public EnemyStateMachine(EnemyController enemy)
    {
        _search = new SearchState(enemy);
    }

    public void Initialized(IEnemyState state)
    {
        _currentState = state;
        state.Enter();
    }

    /// <summary>
    /// State��ύX����ۂɌĂт������\�b�h
    /// </summary>
    /// <param name="nextState">�ύX����State</param>
    public void TransitionState(IEnemyState nextState)
    {
        _currentState.Exit();
        _currentState = nextState;
        nextState.Enter();
    }

    /// <summary>
    /// ���݂�State��Update�������s�����\�b�h
    /// </summary>
    public void Update(float deltaTime)
    {
        if (_currentState != null)
        {
            _currentState.Update(deltaTime);
        }
    }
}
