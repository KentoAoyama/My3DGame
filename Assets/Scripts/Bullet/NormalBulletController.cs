using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UniRx;
using System;

public class NormalBulletController : MonoBehaviour
{
    [Tooltip("�e�̃X�s�[�h")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [Tooltip("�e���j�������܂ł̎���")]
    [SerializeField]
    private int _destroyInterval = 10;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private TrailRenderer _trail;

    private readonly Speed _speed = new();

    private IObjectPool<NormalBulletController> _objectPool;

    Coroutine _delayCoroutine;

    IDisposable _disposable;

    /// <summary>
    /// �������ɌĂяo�����\�b�h
    /// </summary>
    public void MoveStart(IObjectPool<NormalBulletController> objectPool)
    {
        //�I�u�W�F�N�g�v�[���̎Q�Ƃ�n��
        if (_objectPool == null) _objectPool = objectPool;
        //���ʕ����ɃX�s�[�h��ݒ�
        if (_disposable == null)
        {
            //�X�s�[�h���ύX����邽�тɂ����K�p����悤�ɂ���
            _speed.SpeedRp
            .Subscribe(
                speed => 
                _rb.velocity = transform.forward * _bulletSpeed * Time.deltaTime * speed);
        }
        //�j�����s���R���[�`�������s
        _delayCoroutine = StartCoroutine(DestoryInterval());
    }

    private IEnumerator DestoryInterval()
    {
        yield return new WaitForSeconds(50f);

        _delayCoroutine = null;
        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        Release();
    }

    private void Release()
    {
        if (_delayCoroutine != null) StopCoroutine(_delayCoroutine);

        // ���������Z�b�g
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _trail.Clear();

        _objectPool.Release(this);
    }
}