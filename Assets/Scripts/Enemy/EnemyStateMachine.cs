using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateMachine
{
    private IEnemyState _currentState;
    public IEnemyState CurrentState => _currentState;

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
