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

    private readonly EnemyStateMachine _stateMachine = new();
    public EnemyStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// UniTask�L�����Z���p��TokenSource
    /// </summary>
    //private readonly CancellationTokenSource _tokenSource = new();

    public void Initialize(PlayerController player, NormalBulletPool bulletPool)
    {
        _player = player;

        //�@�\���Ƃ̃N���X��������
        _attacker.Initialize(_player, _animator, bulletPool);
        _mover.Initialize(_player, _navMesh);
        _searcher.Initialize(_player);

        //StateMachine�����������AState��ݒ�
        _stateMachine.Initialized(new SearchState(this));
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _attacker.SetIK();
    }

    public void ManualUpdate(float deltaTime)
    {
        //����State��Update�����s
        _stateMachine.Update(deltaTime);
    }

    public void ChangeIKWeight(float weight)
    {
        _attacker.ChangeIKWeight(weight);
    }

    /// <summary>
    /// �U�����s���AUpdate�Ŏ��s���郁�\�b�h
    /// </summary>
    public void Attack(float deltaTime)
    {
        _attacker.Attack(deltaTime);
    }

    public void AttackStop()
    {
        _attacker.AttackStop();
    }

    /// <summary>
    /// �U�����s���AState�̑J�ڎ��Ɏ��s���郁�\�b�h
    ///// </summary>
    //public void AttackStart()
    //{
    //    //�U������莞�Ԃ��Ƃɍs��UniTask�����s���AForget()�Ōx���𖳎�
    //    _attacker.Attack(_tokenSource.Token).Forget();
    //}

    ///// <summary>
    ///// �U�������̎��s���~�߂�
    ///// </summary>
    //public void AttackStop()
    //{
    //    //���s���Ă���UniTask�̍U���������L�����Z������
    //    _tokenSource.Cancel();
    //}

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
