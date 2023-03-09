using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GUIPresenter : MonoBehaviour
{
    [Tooltip("�N���X�w�A���Ǘ�����N���X")]
    [SerializeField]
    private CrossHairController _crossHair;

    [Tooltip("HelpMenu���Ǘ�����N���X")]
    [SerializeField]
    private HelpController _help;

    [Tooltip("Score��Text���Ǘ�����N���X")]
    [SerializeField]
    private ScoreTextController _scoreTextController;

    [Tooltip("Result�̃p�l���𐧌䂷��N���X")]
    [SerializeField]
    private ResultController _result;
    public ResultController Result => _result; //TODO:�v���p�e�B�̑��݂�Y��ăK�b�c�����ݎQ�Ƃ��o���Ă�̂ŁA�C������


    /// <summary>
    /// Score�̒l���Ǘ�����N���X
    /// </summary>
    private ScoreController _scoreController;

    public HelpController Help => _help;

    private PlayerController _player;

    private readonly Speed _speed = new (SpeedType.UI);

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
    /// </summary>
    private IInputProvider _input;

    public void Initialize(
        PlayerController player, 
        IInputProvider input, 
        SpeedController speedController,
        ScoreController scoreController)
    {
        _player = player;
        _input = input;
        _scoreController = scoreController;

        _scoreTextController.Initialize();
        _crossHair.Initialize(_player.Shooter.ShootInterval);
        _help.Initialized(speedController);
        _result.Initialize();

        //�ˌ����Ɏ��s�����C�x���g�ɓo�^����
        _player.Shooter.OnBulletShoot += _crossHair.RotateCrossHair;

        _speed.SpeedRp
            .Subscribe(TweenTimeChange)
            .AddTo(gameObject);

        _scoreController.Score
            .Skip(1)
            .Subscribe(_scoreTextController.SetScoreText)
            .AddTo(gameObject);
    }

    private void TweenTimeChange(float speed)
    {
        _crossHair.TweenTimeChange(speed);
    }

    public void ManualUpdate(InGameState gameState)
    {
        if (_input.GetEscape() && gameState != InGameState.Finish)
        {
            if (gameState != InGameState.Pause)
            {
                _help.OpenHelp(gameState);
            }
            else
            {
                _help.CloseHelp(gameState);
            }
        }
    }

    private void OnDisable()
    {
        _player.Shooter.OnBulletShoot -= _crossHair.RotateCrossHair;
    }
}
