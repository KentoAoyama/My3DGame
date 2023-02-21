using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

[System.Serializable]
public class EnemyDeath
{
    [Header("���̓����蔻��")]
    [SerializeField]
    private Collider _head;


    [Header("�E�̓�̘r�̓����蔻��")]
    [SerializeField]
    private Collider _rightArm;

    [Header("�E�r�̓����蔻��")]
    [SerializeField]
    private Collider _rightForeArm;

    [Header("�E��̓����蔻��")]
    [SerializeField]
    private Collider _rightHand;


    [Header("���̓�̘r�̓����蔻��")]
    [SerializeField]
    private Collider _leftArm;

    [Header("���r�̓����蔻��")]
    [SerializeField]
    private Collider _leftForeArm;

    [Header("����̓����蔻��")]
    [SerializeField]
    private Collider _leftHand;


    [Header("�E��r�̓����蔻��")]
    [SerializeField]
    private Collider _rightUpLeg;

    [Header("�E�r�̓����蔻��")]
    [SerializeField]
    private Collider _rightLeg;


    [Header("����r�̓����蔻��")]
    [SerializeField]
    private Collider _leftUpLeg;

    [Header("���r�̓����蔻��")]
    [SerializeField]
    private Collider _leftLeg;


    [Header("��U�����̃p�����[�^�[")]
    [Tooltip("�m�b�N�o�b�N�̍ۂɉ������")]
    [SerializeField]
    private float _knockBackPower = 10f;

    [Tooltip("�ݒ肷��RigidBody��angularDrag�̒l")]
    [SerializeField]
    private float _angularDrag = 30f;

    [Tooltip("���S���Ă���v�[���ɖ߂��܂ł̎���")]
    [SerializeField]
    private float _interval = 5f;

    private float _timer = 0f;

    private List<Collider> _colliders = new();
    private Transform[] _childrenObjects;
    private Transform[] _copyChildrenObjects;
    private Rigidbody[] _rbs;

    private Animator _animator;
    private NavMeshAgent _navMesh;
    private IObjectPool<EnemyController> _objectPool;
    private EnemyController _enemy;
    private Speed _speed;

    /// <summary>
    /// ���ݏ����Ă���R���C�_�[
    /// </summary>
    private Collider _deletedCollider;

    public void Initialize(Animator animator, NavMeshAgent navMesh, EnemyPool pool, EnemyController enemy, Speed speed)
    {
        _animator = animator;
        _navMesh = navMesh;
        _objectPool = pool.Pool;
        _enemy = enemy;
        _speed = speed;

        //layer��ύX���邽�ߑS�ẴI�u�W�F�N�g���擾
        _childrenObjects = _enemy.gameObject.transform.GetComponentsInChildren<Transform>();

        //���O�h�[�����̋��������̂���RigidBody�����ׂĎ擾
        _rbs = _enemy.transform.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in _rbs)
        {
            rb.angularDrag = _angularDrag;
            rb.useGravity = false;
        }

        //���ʔj����s���p�[�c��List�ɒǉ�
        _colliders.Add(_head);
        _colliders.Add(_rightArm);
        _colliders.Add(_rightForeArm);
        _colliders.Add(_rightHand);
        _colliders.Add(_leftArm);
        _colliders.Add(_leftArm);
        _colliders.Add(_leftForeArm);
        _colliders.Add(_rightUpLeg);
        _colliders.Add(_rightLeg);
        _colliders.Add(_leftUpLeg);
        _colliders.Add(_leftLeg);
    }

    public void KnockBack(Collider collider, Vector3 dir)
    {
        Debug.Log("KnockBack");
        BodyBreak(collider);

        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();

        rb.AddForce(dir * _knockBackPower, ForceMode.Impulse);
    }

    private void BodyBreak(Collider collider)
    {
        if (_colliders.Contains(collider))
        {
            collider.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            _deletedCollider = collider;
        }
    }

    public void Dead()
    {
        foreach (Transform child in _childrenObjects)
        {
            child.gameObject.layer = 8;
        }
        _animator.enabled = false;
        _navMesh.enabled = false;
        
    }

    public void Revive()
    {
        _timer = 0f;

        if (_deletedCollider != null)
        {
            _deletedCollider.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _deletedCollider = null;
        }
        
        foreach (Transform child in _childrenObjects)
        {
            child.gameObject.layer = 6;
        }
        _navMesh.enabled = true;
        _animator.enabled = true;
    }

    public void DeadUpdate(float deltaTime)
    {
        _timer += deltaTime;

        foreach (var rb in _rbs)
        {
            rb.AddForce(Physics.gravity * _speed.CurrentSpeed * deltaTime * 200);
        }

        if (_timer > _interval)
        {
            _objectPool.Release(_enemy);
            _enemy.StateMachine.TransitionState(new IdleState());
        }
    }
}
