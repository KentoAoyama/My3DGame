using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    [Tooltip("�v���C���[�̃N���X")]
    [SerializeField]
    private PlayerController _player;

    [Tooltip("�G�̐������s���N���X")]
    [SerializeField]
    private EnemyGenerator _enemyGenerator;

    [Tooltip("�e�̃I�u�W�F�N�g�v�[��")]
    [SerializeField]
    private NormalBulletPool _bulletPool;

    [Tooltip("�G�̃I�u�W�F�N�g�v�[��")]
    [SerializeField]
    private EnemyPool _enemyPool;

    [Tooltip("GUI�̊Ǘ�������N���X")]
    [SerializeField]
    private GUIPresenter _gui;

    void Start()
    {
        CursorInit();
        _player.Initialize(_bulletPool);
        _enemyGenerator.Initialize(_player, _bulletPool, _enemyPool);
        _gui.Initialize(_player);
    }

    private void CursorInit()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _player.ManualUpdate(deltaTime);
        _enemyGenerator.ManualUpdate(deltaTime);
    }
}
