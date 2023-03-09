using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerShoot
{
    [Header("�X�e�[�^�X�֘A")]

    [Tooltip("Ray�̍ő�̒���")]
    [SerializeField]
    private float _rayLength = 100f;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private float _shootInterval = 1f;
    public float ShootInterval => _shootInterval;

    [Tooltip("�e�̃v���n�u")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("�e���̈ʒu")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("�N���X�w�A��Image")]
    [SerializeField]
    private Image _crassHair;

    [SerializeField]
    private AudioSource _audio;

    public event Action OnBulletShoot;

    private ObjectPoolsController _objectPool;
    private Speed _speed;

    private float _shootIntervalTimer = 0f;

    public void Initialize(ObjectPoolsController objectPool, Speed speed)
    {
        _objectPool = objectPool;
        _speed = speed;
    }

    /// <summary>
    /// �v���C���[�̎ˌ�����
    /// </summary>
    /// <param name="isShoot">�ˌ����s�����ǂ���</param>
    public void BulletShoot(bool isShoot, float deltaTime) //TODO�FUniRx�ł̃C���^�[�o������������Ă݂�
    {
        //�C���^�[�o���ɃJ�E���g�����Z
        _shootIntervalTimer += deltaTime * _speed.CurrentSpeed;

        if (isShoot && _shootInterval < _shootIntervalTimer)
        {
            //event�����s
            OnBulletShoot?.Invoke();
            _audio.Play();

            //�ꍇ�ɉ������e�𐶐�
            NormalBulletController bulletController = _objectPool.BulletPool.Pool.Get();

            GameObject bullet = bulletController.gameObject;
            bullet.transform.position = _muzzle.position;

            Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
            // Ray�������A�������Ă����炻�̍��W�Ɍ�����
            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            {             
                bullet.transform.forward = hit.point - _muzzle.transform.position;
            }
            //�������Ă��Ȃ���΁ARay�̏I���_�Ɍ������Č���
            else
            {
                bullet.transform.forward = Camera.main.transform.forward * _rayLength - _muzzle.transform.position;
            }

            //�e�𓮂����A�I�u�W�F�N�g�v�[���̎Q�Ƃ�n��
            bullet.GetComponent<NormalBulletController>()
                .MoveStart(_objectPool);

            //�C���^�[�o�������Z�b�g
            _shootIntervalTimer = 0f;
        }
    }
}
