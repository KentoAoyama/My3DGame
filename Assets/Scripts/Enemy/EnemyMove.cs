using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyMove
{
    [Tooltip("�G�̈ړ����x")]
    [SerializeField]
    private float _moveSpeed = 1f;

    [Tooltip("SoundEffect���o��Ԋu")]
    [SerializeField]
    private float _soundInterval = 3f;

    private EnemyController _enemy;

    private PlayerController _player;

    private NavMeshAgent _navMesh;

    private SoundEffectPool _soundEffectPool;

    private float _timer = 0f;

    public void Initialize(EnemyController enemy, PlayerController player, NavMeshAgent navMesh, SoundEffectPool soundEffectPool)
    {
        _enemy = enemy;
        _player = player;
        _navMesh = navMesh;
        _soundEffectPool = soundEffectPool;

        _timer = _soundInterval;
    }

    public void Move(float deltaTime)
    {
        _navMesh
            .SetDestination(
            _player.gameObject.transform.position);

        if (_navMesh.speed != _moveSpeed) _navMesh.speed = _moveSpeed;

        //��莞�Ԃ��ƂɃT�E���h�G�t�F�N�g�𐶐�
        _timer += deltaTime;

        if (_timer > _soundInterval)
        {
            SoundEffect soundEffect = _soundEffectPool.Pool.Get();
            soundEffect.Initialize(_soundEffectPool, SoundEffectType.Danger, _enemy.transform.position);

            _timer = 0;
        }
    }
}
