using UnityEngine;

public interface IInputProvider
{
    /// <summary>
    /// �ړ������̓��͏���
    /// </summary>
    /// <returns>�ړ��̕���</returns>
    Vector2 GetMoveDir();

    /// <summary>
    /// �U���̓��͏���
    /// </summary>
    /// <returns>�U���̓��͔���</returns>
    bool GetFire();

    /// <summary>
    /// �ڂ������͏���
    /// </summary>
    /// <returns>�ڂ������͔���</returns>
    bool GetCloseEye();
}
