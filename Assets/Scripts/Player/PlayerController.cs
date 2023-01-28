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

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
    /// </summary>
    [Inject]
    private IInputProvider _input;


    public void Initialize()
    {
        _mover.Initialize(_rb, transform);
    }

    public void ManualUpdate(float deltaTime)
    {
        _mover.Move(_input.GetMoveDir());
        _shooter.BulletShoot(_input.GetFire(), deltaTime);
    }
}
