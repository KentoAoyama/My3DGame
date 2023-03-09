using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour, IHittable
{
    [Header("�R���|�[�l���g")]

    [Tooltip("�v���C���[��RigidBody")]
    [SerializeField]
    private Rigidbody _rb;

    [Tooltip("�v���C���[��Animator")]
    [SerializeField]
    private Animator _animator;


    [Header("�@�\���Ƃ̃N���X")]

    [SerializeField]
    private PlayerMove _mover;

    [SerializeField]
    private PlayerShoot _shooter;

    [SerializeField]
    private PlayerCloseEye _closeEye;

    public PlayerShoot Shooter => _shooter;

    private readonly Speed _speed = new(SpeedType.Player);

    private bool _isHit;
    public bool IsHit => _isHit;

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
    /// </summary>
    private IInputProvider _input;
    public IInputProvider Input => _input;

    public void Initialize(
        ObjectPoolsController objectPool,
        SpeedController speedController,
        IInputProvider input)
    {
        _input = input;
        _mover.Initialize(_rb, transform, _speed);
        _shooter.Initialize(objectPool, _speed);
        _closeEye.Initialize(this, speedController);

        _speed.SpeedRp
            .Subscribe(ChangeSpeed)
            .AddTo(gameObject);
    }

    /// <summary>
    /// Speed�̒l���ω������ۂɌĂяo����郁�\�b�h
    /// </summary>
    private void ChangeSpeed(float speed)
    {
        if (_animator)
        _animator.speed = speed;
    }

    public void ManualUpdate(float deltaTime)
    {
        _mover.Move(_input.GetMoveDir());
        _shooter.BulletShoot(_input.GetFire(), deltaTime);
    }

    private void OnDisable()
    {
        //�K�X�폜����
        _speed.Unsubscribe();
    }

    public void Hit()
    {
        _isHit = true;
    }
}
