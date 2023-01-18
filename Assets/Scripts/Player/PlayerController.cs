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


    private void Start()
    {
        _mover.Initialize(_rb, transform);
        _shooter.Initialize(transform);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _mover.Move(_input.GetMoveDir(), deltaTime);
        _shooter.BulletShoot(_input.GetFire(), deltaTime);
    }
}
