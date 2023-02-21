using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NormalBulletController : MonoBehaviour
{
    [Tooltip("�e�̃X�s�[�h")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [Tooltip("�e���j�������܂ł̎���")]
    [SerializeField]
    private float _destroyInterval = 10f;

    [SerializeField]
    private TrailRenderer _trail;

    private readonly Speed _speed = new();

    private IObjectPool<NormalBulletController> _objectPool;

    Coroutine _delayCoroutine;

    private void Update()
    {
        //�O�Ɉړ�����������
        transform.Translate(
            transform.forward * _bulletSpeed * Time.deltaTime * _speed.CurrentSpeed,
            Space.World);
    }

    /// <summary>
    /// �������ɌĂяo�����\�b�h
    /// </summary>
    public void MoveStart(IObjectPool<NormalBulletController> objectPool)
    {
        //�I�u�W�F�N�g�v�[���̎Q�Ƃ�n��
        if (_objectPool == null) _objectPool = objectPool;

        //�j�����s���R���[�`�������s
        _delayCoroutine = StartCoroutine(DestoryInterval());
    }

    private IEnumerator DestoryInterval()
    {
        yield return new WaitForSeconds(_destroyInterval);

        _delayCoroutine = null;
        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        //���������ۂ̏�������`����Ă��邩�`�F�b�N
        if (other.transform.root.TryGetComponent(out IHittable hit))
        {
            hit.Hit();
        }

        //�m�b�N�o�b�N��������`����Ă��邩�`�F�b�N
        if (other.transform.root.TryGetComponent(out IKnockBackable knockBackable))
        {
            knockBackable.KnockBack(other, transform.forward);
        }

        Release();
    }

    private void Release()
    {
        if (_delayCoroutine != null) StopCoroutine(_delayCoroutine);

        _trail.Clear();

        _objectPool.Release(this);
    }
}
