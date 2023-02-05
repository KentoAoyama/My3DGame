using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack
{
    [Tooltip("�e�̃v���n�u")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("�e����Transform")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("��� Position �ɑ΂���E�F�C�g")]
    [SerializeField, Range(0f, 1f)] 
    private float _handPositionWeight = 0;

    [Tooltip("��� Rotation �ɑ΂���E�F�C�g")]
    [SerializeField, Range(0f, 1f)]
    private float _handRotationWeight = 0;

    private PlayerController _player;

    private Animator _animator;

    public void Initialize(PlayerController player, Animator animator)
    {
        _player = player;
        _animator = animator;
    }

    public void SetIK()
    {
        _animator.SetIKPosition(
            AvatarIKGoal.RightHand,
            _player.transform.position);

        _animator.SetIKRotation(
            AvatarIKGoal.RightHand,
            _player.transform.rotation);

        _animator.SetIKPositionWeight(
            AvatarIKGoal.RightHand,
            _handPositionWeight);

        _animator.SetIKRotationWeight(
            AvatarIKGoal.RightHand, 
            -_handRotationWeight);         
    }

    public void ChangeIKWeight()
    {
        _handPositionWeight = 1f;
        _handRotationWeight = 1f;
    }

    public void Attack()
    {

    }
}
