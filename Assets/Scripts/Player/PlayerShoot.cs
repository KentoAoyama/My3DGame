using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShoot
{
    [Tooltip("Ray�̍ő�̒���")]
    [SerializeField]
    private float _rayLength = 100f;

    [SerializeField]
    private float _shootInterval = 1f;

    [Tooltip("�e�̃v���n�u")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("�e���̈ʒu")]
    [SerializeField]
    private Transform _muzzle;

    private float _shootIntervalTimer = 0f;

    Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// �v���C���[�̎ˌ�����
    /// </summary>
    /// <param name="isShoot">�ˌ����s�����ǂ���</param>
    public void BulletShoot(bool isShoot, float deltaTime)
    {
        _shootIntervalTimer += deltaTime;

        if (!isShoot && _shootInterval < _shootIntervalTimer) return;

        Physics.Raycast(new Ray(_transform.position, _transform.forward), out RaycastHit hit, _rayLength);

        var bullet = GameObject.Instantiate(_bullet, _muzzle.position, default);
        bullet.transform.forward = hit.transform.position - _muzzle.transform.position;

        _shootIntervalTimer = 0f;
    }
}
