using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBreak : MonoBehaviour, IHittable
{
    [Tooltip("���̕��ʂ��Ƃ̃R���C�_�[")]
    [SerializeField]
    Collider _collider;

    private bool _isHit;
    public bool IsHit => _isHit;

    /// <summary>
    /// �U���������ɍs������
    /// </summary>
    public void Hit()
    {
        _isHit = true;
    }
}
