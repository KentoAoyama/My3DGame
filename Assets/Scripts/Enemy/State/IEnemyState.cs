using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    /// <summary>
    /// ����State�ɕύX���ꂽ�ۂɎ��s����
    /// </summary>
    void Enter() { }

    /// <summary>
    /// ����State��Update����
    /// </summary>
    void Update() { }

    /// <summary>
    /// ����State��Update����
    /// </summary>
    /// <param name="deltaTime"></param>
    void Update(float deltaTime) { }

    /// <summary>
    /// ����State����ʂ�State�ɕύX���ꂽ�ۂɎ��s����
    /// </summary>
    void Exit() { }
}
