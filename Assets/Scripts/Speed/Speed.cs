using System;
using UnityEngine;
using UniRx;

/// <summary>
/// ���x�Ɋւ���Float�^�̒l����舵�����߂̃N���X
/// </summary>
public class Speed
{
    // �l�̕ύX���w�ǂł���悤ReactiveProperty�Œ�`
    private ReactiveProperty<float> _speed = new(1f);

    /// <summary>
    /// �X�s�[�h��ReactiveProperty
    /// </summary>
    public IReadOnlyReactiveProperty<float> SpeedRp => _speed;

    /// <summary>
    /// ���݂̃X�s�[�h�̒l
    /// </summary>
    public float CurrentSpeed => _speed.Value;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public Speed()
    {
        SpeedManager.Instance.Subscribe(this);
    }

    public void ChangeSpeed(float speed)
    {
        _speed.Value = speed;
    }

    /// <summary>
    /// SpeedManager�N���X��List����A���̃N���X�̓o�^���폜����
    /// </summary>
�@�@public void Unsubscribe()
    {
        SpeedManager.Instance.Unsubscribe(this);
    }
}
