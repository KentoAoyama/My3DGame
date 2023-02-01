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
    private float _gravity;

    private PlayerController _player;
    public PlayerController Player => _player;

    private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine => _stateMachine;

    private void Start()
    {
        //���u����Find����
        _player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        //StateMachine�����������AState��ݒ�
        _stateMachine = new (this);
        _stateMachine.Initialized(_stateMachine.Search);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _stateMachine.Update(deltaTime);

        Physics.gravity = new Vector3(0, -_gravity, 0);
    }

    public void Move()
    {
        _navMesh
            .SetDestination(
            Player.gameObject.transform.position);
    }
}
