using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Header("�R���|�[�l���g")]

    [Tooltip("�v���C���[��RigidBody")]
    [SerializeField]
    private Rigidbody _rb;


    [Header("�@�\���Ƃ̃N���X")]

    [SerializeField]
    private PlayerMove _mover;

    [SerializeField]
    private PlayerShoot _shooter;

    [SerializeField]
    private PlayerCloseEye _closeEye;

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
    /// </summary>
    [Inject]
    private IInputProvider _input;
    public IInputProvider Input => _input;

    public void Initialize(NormalBulletPool bulletPool)
    {
        _mover.Initialize(_rb, transform);
        _shooter.Initialize(bulletPool);
        _closeEye.Initialize(this);
    }

    public void ManualUpdate(float deltaTime)
    {
        _mover.Move(_input.GetMoveDir());
        _shooter.BulletShoot(_input.GetFire(), deltaTime);
    }
}
