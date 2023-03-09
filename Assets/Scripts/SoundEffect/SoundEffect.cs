using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Pool;

public enum SoundEffectType
{
    Normal,
    Danger1,
    Danger2
}

public class SoundEffect : MonoBehaviour
{
    [Header("�ʏ�̃G�t�F�N�g�i�j�̃C���^�[�o��")]
    [SerializeField]
    private float _startIntervalN = 0.5f;

    [SerializeField]
    private float _finishIntervalN = 3f;

    [Header("�ʏ�̃G�t�F�N�g�i�ԁj�̃C���^�[�o��")]
    [SerializeField]
    private float _startIntervalD = 0.5f;

    [SerializeField]
    private float _finishIntervalD = 3f;

    [Header("���ʂ��Ďg���ݒ�")]
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private Material _normalMaterial;

    [SerializeField]
    private Material _dangerMaterial;

    [SerializeField]
    private AudioSource _danger1Audio;

    [SerializeField]
    private AudioSource _danger2Audio;

    private IObjectPool<SoundEffect> _objectPool;

    private float _startInterval = 0f;
    private float _finishInterval = 0f;

    public void Initialize(IObjectPool<SoundEffect> objectPool, SoundEffectType type, Vector3 position)
    {
        if (_objectPool == null)_objectPool = objectPool;

        //���̃^�C�v���Ƃɒl��ݒ肷��
        switch(type)
        {
            case SoundEffectType.Normal:
                _meshRenderer.material = _normalMaterial;
                gameObject.layer = 10;
                _startInterval = _startIntervalN;
                _finishInterval = _finishIntervalN;
                break;
            case SoundEffectType.Danger1:
                _meshRenderer.material = _dangerMaterial;
                gameObject.layer = 9;
                _startInterval = _startIntervalD;
                _finishInterval = _finishIntervalD;
                _danger1Audio.Play();

                break;
            case SoundEffectType.Danger2:
                _meshRenderer.material = _dangerMaterial;
                gameObject.layer = 9;
                _startInterval = _startIntervalD;
                _finishInterval = _finishIntervalD;
                _danger2Audio.Play();

                break;
        }

        transform.position = position;
        transform.localScale = new Vector3(0f, 0f, 0f);
        StartSound();
    }

    private void StartSound()
    {
        var sequence = DOTween.Sequence();
        sequence.Insert(0f, transform.DOScale(1f, _startInterval))
            .Insert(_startInterval, transform.DOScale(0f, _finishInterval))
            .Play()
            .OnComplete(() => _objectPool.Release(this));
    }

    private void Update()
    {
        //��Ƀv���C���[�̂ق��������悤�ɂ���
        transform.LookAt(Camera.main.transform.position);
    }
}
