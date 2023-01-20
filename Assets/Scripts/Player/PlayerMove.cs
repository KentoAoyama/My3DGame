using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("�ړ��̑��x")]
    [SerializeField]
    private float _moveSpeed = 1000f;

    private readonly Speed _speed = new();

    private Rigidbody _rb;
    private Transform _transform;

    public void Initialize(Rigidbody rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    /// <param name="moveDir">�ړ�����</param>
    /// <param name="deltaTime">���x��Update���ł��ς��Ȃ��悤�ɂ��邽�߂Ɏg�p����deltaTime</param>
    public void Move(Vector2 moveDir, float deltaTime)
    {
        //�ړ�����������v�Z
        Vector3 dir = Vector3.forward * moveDir.y + Vector3.right * moveDir.x;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        //���ʂ�K�p
        float moveSpeed = _moveSpeed * _speed.CurrentSpeed;
        _rb.velocity = dir.normalized * moveSpeed * deltaTime + Vector3.up * _rb.velocity.y * deltaTime;

        _transform.rotation = new Quaternion(
            0,
            Camera.main.transform.rotation.y,
            0,
            Camera.main.transform.rotation.w);
    }
}
