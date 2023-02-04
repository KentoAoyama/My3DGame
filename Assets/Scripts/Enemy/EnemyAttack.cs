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
    private Transform _muzzle;

    [Tooltip("���Transform")]
    private Transform _hand;

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

    public void SetIK(Animator animator)
    {
        _animator.SetIKPosition(
            AvatarIKGoal.RightHand, 
            _hand.position);

        _animator.SetIKRotation(
            AvatarIKGoal.RightHand, 
            _hand.rotation);

        _animator.SetIKPositionWeight(
            AvatarIKGoal.RightHand,
            _handPositionWeight);

        _animator.SetIKRotationWeight(
            AvatarIKGoal.RightHand, 
            _handRotationWeight);         
    }

    public void AttackIKSet()
    {
        //_animator.SetLayerWeight(2, )
    }

    public void Attack()
    {

    }
}
