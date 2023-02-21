using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("�����Ɋւ���ݒ�")]

    [Tooltip("����������W")]
    [SerializeField]
    private Transform[] _generatePos;

    [Tooltip("�ǂ̏��Ԃłǂ̍��W���琶�����邩�w�肷��z��")]
    [Range(0, MAX_POS_LENGTH)]
    [SerializeField]
    private int[] _generatePosIndex;

    [Tooltip("�����̃C���^�[�o��")]
    [SerializeField]
    private float _interval = 5f;

    [Tooltip("�������s����")]
    [SerializeField]
    private int _generateTime = 5;

    private const int MAX_POS_LENGTH = 10;

    private float _timer = 0;
    private int _indexCount = 0;
    private int _generateCount = 0;

    private PlayerController _player;
    private NormalBulletPool _bulletPool;
    private EnemyPool _enemyPool;

    /// <summary>
    /// ���������G��ێ����郊�X�g
    /// </summary>
    private List<EnemyController> _enemys = new();

    public void Initialize(PlayerController player, NormalBulletPool bulletPool, EnemyPool enemyPool)
    {
        //�z��̒������I�[�o�[���Ă��Ȃ����`�F�b�N
        if (_generatePos.Length > MAX_POS_LENGTH)
        {
            Debug.LogError($"�w�肵�����W�̔z��{MAX_POS_LENGTH}��蒷���Ȃ��Ă��܂�");
        }

        //�w�肵���C���f�b�N�X�̗v�f�����邩����
        foreach (int n in _generatePosIndex)
        {
            if (n > _generatePos.Length)
                Debug.LogError("GeneratePosIndex�̗v�f���A�z��̗v�f�O���w�肵�Ă��܂�");
        }

        //InGameController����Q�Ƃ��󂯎��
        _player = player;
        _bulletPool = bulletPool;
        _enemyPool = enemyPool;
    }

    public void ManualUpdate(float deltaTime)
    {
        Generate(deltaTime);
        EnemysUpdate(deltaTime);
    }

    private void Generate(float deltaTime)
    {
        //�w�肵���񐔈ȏ�͐������Ȃ�
        if (_generateCount >= _generateTime) return;

        _timer += deltaTime;

        if (_timer > _interval)
        {
            //�G�𐶐�
            EnemyController enemy = _enemyPool.Pool.Get();
            //�܂��ǉ�����Ă��Ȃ����List�ɒǉ��A����������
            if (!_enemys.Contains(enemy)) 
            {
                _enemys.Add(enemy);
                enemy.Initialize(_player, _bulletPool, _enemyPool);
            }
            //���ɒǉ�����Ă�����State��ύX����
            else
            {
                enemy.StateMachine.TransitionState(new SearchState(enemy));
            }
            //�w�肵�����W�Ɉړ�
            enemy.transform.position =
                _generatePos[_generatePosIndex[_indexCount % _generatePosIndex.Length]].position;

            _indexCount++;
            _generateCount++;
            _timer = 0;
        }
    }

    private void EnemysUpdate(float deltaTime)
    {
        if (_enemys.Count > 0)
        {
            foreach (var enemy in _enemys)
            {
                enemy.ManualUpdate(deltaTime);
            }
        }
    }
}
