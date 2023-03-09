using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsController : MonoBehaviour
{
    [Header("�e�̃I�u�W�F�N�g�v�[��")]
    [SerializeField]
    private ObjectPool<NormalBulletController> _bulletPool;
    /// <summary>
    /// �e�̃I�u�W�F�N�g�v�[��
    /// </summary>
    public ObjectPool<NormalBulletController> BulletPool => _bulletPool;

    [Header("�G�̃I�u�W�F�N�g�v�[��")]
    [SerializeField]
    private ObjectPool<EnemyController> _enemyPool;
    /// <summary>
    /// �e�̃I�u�W�F�N�g�v�[��
    /// </summary>
    public ObjectPool<EnemyController> EnemyPool => _enemyPool;

    [Header("�T�E���h�G�t�F�N�g�̃I�u�W�F�N�g�v�[��")]
    [SerializeField]
    private ObjectPool<SoundEffect> _soundEffectPool;
    /// <summary>
    /// �T�E���h�G�t�F�N�g�̃I�u�W�F�N�g�v�[��
    /// </summary>
    public ObjectPool<SoundEffect> SoundEffectPool => _soundEffectPool;
}
