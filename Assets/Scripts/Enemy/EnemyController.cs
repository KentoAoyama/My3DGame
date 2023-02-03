using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Tooltip("NavmeshAgent�R���|�[�l���g")]
    [SerializeField]
    private NavMeshAgent _navMesh;

    [SerializeField]
    private float _gravity = 9.8f;

    [SerializeField, Range(0f, 0.02f)]
    private float _fixedUpdate = 0.02f;


    [Header("�@�\���Ƃ̃N���X")]

    [SerializeField]
    private EnemyAttack _attacker;

    [SerializeField]
    private EnemyMove _mover;

    private PlayerController _player;

    private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine => _stateMachine;

    private void Start()
    {
        //���u����Find����
        _player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        //�@�\���Ƃ̃N���X��������
        _attacker.Initialize(_player);
        _mover.Initialize(_player, _navMesh);

        //StateMachine�����������AState��ݒ�
        _stateMachine = new (this);
        _stateMachine.Initialized(_stateMachine.Search);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _stateMachine.Update(deltaTime);
    }

    public void Move()
    {
        _mover.Move();
    }
}
