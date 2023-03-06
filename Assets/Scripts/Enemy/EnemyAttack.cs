using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class EnemyAttack
{
    [Header("�e�̃v���p�e�B")]

    [Tooltip("�e�̃v���n�u")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("�e����Transform")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("�U���̃C���^�[�o��")]
    [SerializeField]
    private float _interval = 3f;


    [Header("IK�̐ݒ�")]

    [Tooltip("��� Position �ɑ΂���E�F�C�g")]
    [SerializeField, Range(0f, 1f)] 
    private float _handPositionWeight = 0;

    [Tooltip("��� Rotation �ɑ΂���E�F�C�g")]
    [SerializeField, Range(0f, 1f)]
    private float _handRotationWeight = 0;

    private PlayerController _player;

    private Animator _animator;

    private SoundEffectPool _soundEffectPool;

    private Speed _speed;

    private float _timer = 0;

    /// <summary>
    /// �e�̃I�u�W�F�N�g�v�[��
    /// </summary>
    private NormalBulletPool _normalBulletPool;

    public void Initialize(PlayerController player,
        Animator animator, 
        NormalBulletPool bulletPool,
        SoundEffectPool soundEffectPool,
        Speed speed)
    {
        _player = player;
        _animator = animator;
        _normalBulletPool = bulletPool;
        _soundEffectPool = soundEffectPool;
        _speed = speed;

        _timer = _interval / 2;
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

    public void ChangeIKWeight(float weight)
    {
        _handPositionWeight = weight;
        _handRotationWeight = weight;
    }

    public void Attack(float deltaTime)
    {
        //�ˌ��̃C���^�[�o��
        _timer += deltaTime * _speed.CurrentSpeed;

        if (_timer > _interval)
        {
            //�ꍇ�ɉ������e�𐶐�
            NormalBulletController bulletController = _normalBulletPool.Pool.Get();

            GameObject bullet = bulletController.gameObject;
            bullet.transform.position = _muzzle.position;
            bullet.transform.forward = _player.transform.position - _muzzle.transform.position;

            //�e�𓮂���
            bullet.GetComponent<NormalBulletController>()
                .MoveStart(_normalBulletPool, _soundEffectPool);

            //���̃G�t�F�N�g�𐶐�
            SoundEffect soundEffect = _soundEffectPool.Pool.Get();
            soundEffect.Initialize(_soundEffectPool, SoundEffectType.Danger2, _muzzle.position);

            _timer = 0;
        }
    }

    public void AttackStop()
    {
        _timer = 0;
    }

    ///// <summary>
    ///// ��莞�Ԃ��ƂɍU�����s��
    ///// </summary>
    //public async UniTask Attack(CancellationToken token)
    //{
    //    token.ThrowIfCancellationRequested();

    //    while (true)
    //    {
    //        Debug.Log("Delay�J�n");

    //        �ˌ��̃C���^�[�o��
    //        await UniTask.Delay(TimeSpan.FromSeconds(_interval), cancellationToken: token);

    //        Debug.Log("�e������");
    //        �ꍇ�ɉ������e�𐶐�
    //        NormalBulletController bulletController = _normalBulletPool.Pool.Get();

    //        GameObject bullet = bulletController.gameObject;
    //        bullet.transform.position = _muzzle.position;
    //        bullet.transform.forward = _player.transform.position - _muzzle.transform.position;

    //        �e�𓮂���
    //        bullet.GetComponent<NormalBulletController>().MoveStart(_normalBulletPool.Pool);
    //    }
    //}
}
