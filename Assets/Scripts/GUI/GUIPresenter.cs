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

    private InGameController _gameController;

    private readonly Speed _speed = new (SpeedType.UI);

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
    /// </summary>
    private IInputProvider _input;

    public void Initialize(
        PlayerController player, 
        IInputProvider input, 
        InGameController gameController,
        SpeedController speedController,
        ScoreController scoreController)
    {
        _player = player;
        _input = input;
        _gameController = gameController;
        _scoreController = scoreController;

        _scoreTextController.Initialize();
        _crossHair.Initialize(_player.Shooter.ShootInterval);
        _help.Initialized(gameController, speedController);
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

    public void ManualUpdate()
    {
        if (_input.GetEscape() && _gameController.GameState != InGameState.Finish)
        {
            if (_gameController.GameState != InGameState.Pause)
            {
                _help.OpenHelp();
            }
            else
            {
                _help.CloseHelp();
            }
        }
    }

    private void OnDisable()
    {
        _player.Shooter.OnBulletShoot -= _crossHair.RotateCrossHair;
    }
}
