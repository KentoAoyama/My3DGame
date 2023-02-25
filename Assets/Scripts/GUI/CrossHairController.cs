using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrossHairController : MonoBehaviour
{
    private const float ROTATE_VALUE = 90f;

    private float _rotateTime = 0f;

    public void Initialize(float rotateTime)
    {
        //��]���鎞�Ԃ��ˌ��̃C���^�[�o���ƍ��킹�邽�߁A�l���󂯎��
        _rotateTime = rotateTime;
    }

    public void RotateCrossHair()
    {
        //�N���X�w�A��90�x��]������
        gameObject.transform
            .DOLocalRotate(
            new Vector3(0f, 0f, (gameObject.transform.localRotation.eulerAngles.z +  ROTATE_VALUE) % 360),
            _rotateTime);
    }
}
