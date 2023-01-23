using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour, IBullet
{
    [Tooltip("�e�̃X�s�[�h")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [Tooltip("�e���j�������܂ł̎���")]
    [SerializeField]
    private int _destroyInterval = 10;

    [SerializeField]
    private Rigidbody _rb;

    /// <summary>
    /// �����ɐڐG���Ă��邩���肷��p�̕ϐ�
    /// </summary>
    bool _isHit;

    private Speed _speed = new();

    private IObjectPool<IBullet> _objectPool;

    // �e��ObjectPool�ւ̎Q�Ƃ�^����p�u���b�N�v���p�e�B
    public IObjectPool<IBullet> ObjectPool { set => _objectPool = value; }

    Coroutine _delayCoroutine;

    /// <summary>
    /// �������ɌĂяo�����\�b�h
    /// </summary>
    public void BulletMove()
    {
        _rb.velocity = transform.forward * _bulletSpeed * _speed.CurrentSpeed * Time.deltaTime;

        _delayCoroutine = StartCoroutine(DestoryInterval());
    }

    private IEnumerator DestoryInterval()
    {
        yield return new WaitForSeconds(5);

        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        _isHit = true;
    }

    private void Release()
    {
        if (_delayCoroutine != null) StopCoroutine(_delayCoroutine);

        // ���������Z�b�g
        _rb.velocity = new Vector3(0f, 0f, 0f);

        _objectPool.Release(this);
    }
}
