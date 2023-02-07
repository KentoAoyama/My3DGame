using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySearch
{
    [Tooltip("���G�͈�")]
    [SerializeField]
    private float _searchLength = 10f;

    private EnemyController _enemy;

    private PlayerController _player;

    public void Initialize(PlayerController player)
    {
        _player = player;
    }


    /// <summary>
    /// �v���C���[��T�������A������Ray�̂Q�ōs��
    /// </summary>
    /// <returns>�v���C���[�������Ă��邩</returns>
    public bool PlayerSearch()
    {
        bool _isSearch = false;
        float distance = (_enemy.transform.position - _player.transform.position).sqrMagnitude;

        //�v���C���[���߂��ɂ�����
        if (distance < _searchLength * _searchLength)
        {
            //Ray�������A���������I�u�W�F�N�g���v���C���[������
            _isSearch = Physics.Raycast(_enemy.transform.position,
                _player.transform.position - _enemy.transform.position,
                out RaycastHit hit, _searchLength)
                && hit.collider.gameObject.GetComponent<PlayerController>();
        }

        return _isSearch;
    }
}
