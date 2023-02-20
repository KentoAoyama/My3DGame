using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
