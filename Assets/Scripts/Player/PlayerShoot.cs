using System.Collections;
using System.Collections.Generic;
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

    [Tooltip("�e�̃v���n�u")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("�e���̈ʒu")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("�N���X�w�A��Image")]
    [SerializeField]
    private Image _crassHair;


    [Header("ObjectPool")]

    [Tooltip("�v�[���̃f�t�H���g�̗e��")]
    [SerializeField]
    private int _poolCapacity = 20;

    [Tooltip("�v�[���̍ő�T�C�Y")]
    [SerializeField]
    private int _poolMaxSize = 50;


    private float _shootIntervalTimer = 0f;

    /// <summary>
    /// �v���C���[�̎ˌ�����
    /// </summary>
    /// <param name="isShoot">�ˌ����s�����ǂ���</param>
    public void BulletShoot(bool isShoot, float deltaTime)
    {
        //�C���^�[�o���ɃJ�E���g�����Z
        _shootIntervalTimer += deltaTime;

        if (isShoot && _shootInterval < _shootIntervalTimer)
        {
            //�e�𐶐�
            Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
            GameObject bullet = Object.Instantiate(_bullet, _muzzle.position, default);            

            // Ray�������A�������Ă����炻�̍��W�Ɍ�����
            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            {             
                bullet.transform.forward = hit.point - _muzzle.transform.position;
            }
            //�������Ă��Ȃ���΁A���܌����Ă�������Ɍ������Č���
            else
            {
                bullet.transform.forward = Camera.main.transform.forward * _rayLength - _muzzle.transform.position;
            }

            //�e�𓮂���
            bullet.GetComponent<IBullet>().BulletMove();

            //�C���^�[�o�������Z�b�g
            _shootIntervalTimer = 0f;
        }
    }

    //private void 
}
