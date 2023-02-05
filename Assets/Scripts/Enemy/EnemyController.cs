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

    [Tooltip("Animator�R���|�[�l���g")]
    [SerializeField]
    private Animator _animator;

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
        _attacker.Initialize(_player, _animator);
        _mover.Initialize(_player, _navMesh);

        //StateMachine�����������AState��ݒ�
        _stateMachine = new (this);
        _stateMachine.Initialized(_stateMachine.Attack);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _attacker.SetIK();
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

    public void ChangeIKWeight()
    {
        _attacker.ChangeIKWeight();
    }

    public void Attack()
    {
        _attacker.Attack();
    }
}
