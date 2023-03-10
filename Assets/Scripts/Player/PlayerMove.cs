using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("移動の速度")]
    [SerializeField]
    private float _moveSpeed = 1000f;

    private Rigidbody _rb;
    private Transform _transform;
    private Speed _speed;

    public void Initialize(Rigidbody rb, Transform transform, Speed speed)
    {
        _rb = rb;
        _transform = transform;
        _speed = speed;
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    /// <param name="moveDir">移動方向</param>
    /// <param name="deltaTime">速度がUpdate内で呼びだしても変わらないようにするために使用するdeltaTime</param>
    public void Move(Vector2 moveDir)
    {
        //移動する方向を計算
        Vector3 dir = Vector3.forward * moveDir.y + Vector3.right * moveDir.x;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        //結果を適用
        float moveSpeed = _moveSpeed * _speed.CurrentSpeed;
        _rb.velocity = dir.normalized * moveSpeed + Vector3.up * _rb.velocity.y;

        _transform.rotation = new Quaternion(
            0,
            Camera.main.transform.rotation.y,
            0,
            Camera.main.transform.rotation.w);
    }
}
