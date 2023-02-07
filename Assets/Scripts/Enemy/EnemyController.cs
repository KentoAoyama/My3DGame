using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using Cysharp.Threading.Tasks;

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

    [SerializeField]
    private EnemySearch _searcher;

    private PlayerController _player;

    private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// UniTask�L�����Z���p��TokenSource
    /// </summary>
    private readonly CancellationTokenSource _tokenSource = new();

    private void Start()
    {
        //���u����Find����
        _player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        //�@�\���Ƃ̃N���X��������
        _attacker.Initialize(_player, _animator);
        _mover.Initialize(_player, _navMesh);
        _searcher.Initialize(_player);

        //StateMachine�����������AState��ݒ�
        _stateMachine = new(this);
        _stateMachine.Initialized(_stateMachine.Search);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _attacker.SetIK();
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        //����State��Update�����s
        _stateMachine.Update(deltaTime);
    }

    public void ChangeIKWeight(float weight)
    {
        _attacker.ChangeIKWeight(weight);
    }

    /// <summary>
    /// �U�����s���AState�̑J�ڎ��Ɏ��s���郁�\�b�h
    /// </summary>
    public void AttackStart()
    {
        //�U������莞�Ԃ��Ƃɍs��UniTask�����s���AForget()�Ōx���𖳎�
        _attacker.Attack(_tokenSource.Token).Forget();
    }

    /// <summary>
    /// �U�������̎��s���~�߂�
    /// </summary>
    public void AttackStop()
    {
        //���s���Ă���UniTask�̍U���������L�����Z������
        _tokenSource.Cancel();
    }

    public void Move()
    {
        //NavMesh���g�p���Ĉړ�������
        _mover.Move();
    }

    public bool PlayerSearch()
    {
        return _searcher.PlayerSearch();
    }
}
