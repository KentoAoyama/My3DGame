using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using Cysharp.Threading.Tasks;

public class EnemyController : MonoBehaviour, IKnockBackable
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
    private EnemyDeath _death;

    [SerializeField]
    private EnemySearch _searcher;

    private PlayerController _player;

    private readonly EnemyStateMachine _stateMachine = new();
    public EnemyStateMachine StateMachine => _stateMachine;

    private Speed _speed = new();

    //private readonly CancellationTokenSource _tokenSource = new();

    public void Initialize(PlayerController player, NormalBulletPool bulletPool, EnemyPool enemyPool)
    {
        _player = player;

        //�@�\���Ƃ̃N���X�̏����������s
        _attacker.Initialize(_player, _animator, bulletPool);
        _mover.Initialize(_player, _navMesh);
        _searcher.Initialize(_player);
        _death.Initialize(_animator, _navMesh, enemyPool, this, _speed);

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

    public void Move()
    {
        //NavMesh���g�p���Ĉړ�������
        _mover.Move();
    }

    public bool PlayerSearch()
    {
        return _searcher.PlayerSearch();
    }

    /// <summary>
    /// IKnockBack�C���^�[�t�F�[�X�ɂ���Ď��������m�b�N�o�b�N����
    /// </summary>
    public void KnockBack(Collider collider, Vector3 dir)
    {
        _death.Dead();
        _death.KnockBack(collider, dir);
        _stateMachine.TransitionState(new DeadState(this));
    }
    

    /// <summary>
    /// �Đ����̏���
    /// </summary>
    public void Revive()
    {
        _death.Revive();
    }

    /// <summary>
    /// ���S���Ɏ��s����Update����
    /// </summary>
    public void DeadUpdate(float deltaTime)
    {
        _death.DeadUpdate(deltaTime);
    }
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
