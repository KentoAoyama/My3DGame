using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPresenter : MonoBehaviour
{
    [Tooltip("�N���X�w�A���Ǘ�����N���X")]
    [SerializeField]
    private CrossHairController _crossHair;

    private PlayerController _player;

    public void Initialize(PlayerController player)
    {
        _player = player;
        _crossHair.Initialize(_player.Shooter.ShootInterval);
        //�ˌ����Ɏ��s�����C�x���g�ɓo�^����
        _player.Shooter.OnBulletShoot += _crossHair.RotateCrossHair;
    }

    private void OnDisable()
    {
        _player.Shooter.OnBulletShoot -= _crossHair.RotateCrossHair;
    }
}
