using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySearch
{
    [Tooltip("���G�͈�")]
    [SerializeField]
    private float _searchLength = 10f;

    [Tooltip("Ray�����n�_�̍��W")]
    [SerializeField]
    private Transform _head;

    [Tooltip("Ray���ڐG����Layer")]
    [SerializeField]
    private LayerMask _layer;

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
        Vector3 _dir = _player.transform.position - _head.transform.position;
        float distance = (_dir).sqrMagnitude;
        Ray ray = new (_head.transform.position, _dir);

        //�v���C���[���߂��ɂ�����
        if (distance < _searchLength * _searchLength)
        {
            //Ray�������A���������I�u�W�F�N�g���v���C���[������
            _isSearch = Physics.Raycast(ray, out RaycastHit hit, _searchLength, _layer)
                && hit.collider.gameObject.GetComponent<PlayerController>();
        }

        return _isSearch;
    }
}
