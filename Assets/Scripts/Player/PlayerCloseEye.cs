using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

[Serializable]
public class PlayerCloseEye
{
    [Tooltip("���قƂȂ�Image")]
    [SerializeField]
    private Image _upEyelids;

    [Tooltip("���قƂȂ�Image")]
    [SerializeField]
    private Image _downEyelids;

    [Tooltip("�ق̊J�ɂ����鎞��")]
    [SerializeField]
    private float _duration = 0.5f;

    [Tooltip("���ɂ������̂��̂������Ȃ����邽�߂̃I�u�W�F�N�g")]
    [SerializeField]
    private GameObject _eyelidObject;

    [Tooltip("�ڂ�����ۂɕω�����X�s�[�h")]
    [SerializeField, Range(0f, 1f)]
    private float _changeSpeed = 0.5f;

    private PlayerController _player;

    private SpeedController _speedController;

    private const float CLOSE_HEIGHT_VALUE = 120f;
    private const float OPEN_HEIGHT_VALUE = 320f;

    public void Initialize(PlayerController player, SpeedController speedController)
    {
        _player = player;
        _speedController = speedController;

        _eyelidObject.SetActive(false);

        player.ObserveEveryValueChanged(c => player.Input.GetCloseEye())
            .Skip(1)
            .ThrottleFirst(TimeSpan.FromSeconds(Mathf.Max(0, _duration - 0.05f)))
            .Subscribe(CloseEye)
            .AddTo(player.gameObject);
    }

    private void CloseEye(bool isClose)
    {
        if (isClose)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void Close()
    {
        EyelidTween(CLOSE_HEIGHT_VALUE).OnComplete(Check);
    }

    private void Open()
    {
        CloseEyelidActive(false);
        EyelidTween(OPEN_HEIGHT_VALUE).OnComplete(Check);
    }

    /// <summary>
    /// �ڂ̊J������ɓ��͂̕ω������邩���߂ă`�F�b�N
    /// </summary>
    private void Check()
    {
        if (_player.Input.GetCloseEye())
        {
            EyelidTween(CLOSE_HEIGHT_VALUE);
        }
        else
        {
            CloseEyelidActive(false);
            EyelidTween(OPEN_HEIGHT_VALUE);
        }
    }

    private Sequence EyelidTween(float value)
    {
        var sequence = DOTween.Sequence();
        //�ق𓮂����āA�Ō�ɕ��؂��Ă��邩���肷��
        return sequence
            .Insert(0f, _upEyelids.rectTransform.DOLocalMoveY(value, _duration))
            .Insert(0f, _downEyelids.rectTransform.DOLocalMoveY(-value, _duration))
            .OnComplete(() => 
            CloseEyelidActive(_upEyelids.rectTransform.localPosition.y == CLOSE_HEIGHT_VALUE));
    }

    /// <summary>
    /// �ق̊J�Ɋւ��鏈��True���Ɩڂ��J���Ă��āAFalse���Ɩڂ����S�ɂ��܂��Ă���
    /// </summary>
    private void CloseEyelidActive(bool isClose)
    {
        _upEyelids.gameObject.SetActive(!isClose);
        _downEyelids.gameObject.SetActive(!isClose);
        _eyelidObject.SetActive(isClose);

        if (isClose)
        {
            Camera.main.cullingMask = -1;
            _speedController.SpeedChange(_changeSpeed, ChangeSpeedType.NonPlayer);
        }
        else
        {
            Camera.main.cullingMask &= ~(1 << 9);
            Camera.main.cullingMask &= ~(1 << 10);
            _speedController.SpeedChange(1f);
        }
    }
}
