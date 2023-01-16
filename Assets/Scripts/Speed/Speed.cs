using System;
using UnityEngine;

/// <summary>
/// ���x�Ɋւ���Float�^�̒l����舵�����߂̃N���X
/// </summary>
public class Speed
{
    private float _speed;

    /// <summary>
    /// ���݂̃X�s�[�h�̒l
    /// </summary>
    public float CurrentSpeed => _speed;

    /// <summary>
    /// �X�s�[�h���ύX���ꂽ�ۂɎ��s����f���Q�[�g
    /// </summary>
    public Action<float> OnSpeedChange;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public Speed(float spped)
    {
        _speed = spped;
        SpeedManager.Instance.Subscribe(this);
    }

    public void Change(float speed)
    {
        _speed = speed;
        OnSpeedChange?.Invoke(speed);
    }

    /// <summary>
    /// SpeedManager�N���X��List����A���̃N���X�̓o�^���폜����
    /// </summary>
�@�@public void Unsubscribe()
    {
        SpeedManager.Instance.Unsubscribe(this);
    }
}
