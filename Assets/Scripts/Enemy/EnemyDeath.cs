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
    //private Transform[] _copyChildrenObjects;
    private Rigidbody[] _rbs;

    private Animator _animator;
    private NavMeshAgent _navMesh;
    private ObjectPoolsController _objectPool;
    private GameObject _enemy;
    private Speed _speed;
    private ScoreController _scoreController;

    /// <summary>
    /// ���ݏ����Ă���R���C�_�[
    /// </summary>
    private Collider _deletedCollider;

    public void Initialize(
        Animator animator,
        NavMeshAgent navMesh,
        ObjectPoolsController objectPool, 
        Speed speed,
        ScoreController scoreController,
        GameObject enemy)
    {
        _animator = animator;
        _navMesh = navMesh;
        _objectPool = objectPool;
        _speed = speed;
        _scoreController = scoreController;

        //layer��ύX���邽�ߑS�ẴI�u�W�F�N�g���擾
        _childrenObjects = enemy.gameObject.transform.GetComponentsInChildren<Transform>();

        //���O�h�[�����̋��������̂���RigidBody�����ׂĎ擾
        _rbs = enemy.transform.GetComponentsInChildren<Rigidbody>();
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

        //�e�������������ʂ̃R���C�_�[�ɗ͂�������
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(dir * _knockBackPower, ForceMode.Impulse);

        BodyBreak(collider);
    }

    private void BodyBreak(Collider collider)
    {
        //���ʔj��\�ȃI�u�W�F�N�g�������ꍇ�s��
        if (_colliders.Contains(collider))
        {
            collider.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            //��Ŗ߂����ߔj�󂵂��I�u�W�F�N�g������Ă���
            _deletedCollider = collider;
        }
    }

    public void Dead()
    {
        //�e�ƃv���C���[�ɐڐG���Ȃ��悤�Ƀ��C���\��ύX����
        foreach (Transform child in _childrenObjects)
        {
            child.gameObject.layer = 8;
        }

        //�A�j���[�^�[��NavMesh��؂��ă��O�h�[����������
        _animator.enabled = false;
        _navMesh.enabled = false;

        _scoreController.AddScore();
    }

    public void Revive()
    {
        _timer = 0f;

        //�j�󂵂Ă��镔�ʂ�����ꍇ���ɖ߂�
        if (_deletedCollider != null)
        {
            _deletedCollider.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _deletedCollider = null;
        }
        
        //�ς��Ă������C���[�����ɖ߂�
        foreach (Transform child in _childrenObjects)
        {
            child.gameObject.layer = 6;
        }
        //�A�j���[�^�[��NavMesh���N������
        _navMesh.enabled = true;
        _animator.enabled = true;
    }

    public void DeadUpdate(float deltaTime, EnemyController enemyController)
    {
        _timer += deltaTime;

        //�d�͂��蓮�ŉ�����
        foreach (var rb in _rbs)
        {
            rb.AddForce(Physics.gravity * _speed.CurrentSpeed * deltaTime * 200);
        }

        //��莞�Ԃ���������v�[���ɃI�u�W�F�N�g��߂��AState��ύX����
        if (_timer > _interval)
        {
            _objectPool.EnemyPool.Pool.Release(enemyController);
            enemyController.StateMachine.TransitionState(new IdleState());
        }
    }
}
