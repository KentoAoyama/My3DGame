using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerShoot
{
    [Header("ステータス関連")]

    [Tooltip("Rayの最大の長さ")]
    [SerializeField]
    private float _rayLength = 100f;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private float _shootInterval = 1f;
    public float ShootInterval => _shootInterval;

    [Tooltip("弾のプレハブ")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("銃口の位置")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("クロスヘアのImage")]
    [SerializeField]
    private Image _crassHair;

    [SerializeField]
    private AudioSource _audio;

    public event Action OnBulletShoot;

    private NormalBulletPool _normalBulletPool;
    private SoundEffectPool _soundEffectPool;
    private Speed _speed;

    private float _shootIntervalTimer = 0f;

    public void Initialize(NormalBulletPool bulletPool, SoundEffectPool soundEffectPool, Speed speed)
    {
        _normalBulletPool = bulletPool;
        _soundEffectPool = soundEffectPool;
        _speed = speed;
    }

    /// <summary>
    /// プレイヤーの射撃処理
    /// </summary>
    /// <param name="isShoot">射撃を行うかどうか</param>
    public void BulletShoot(bool isShoot, float deltaTime) //TODO：UniRxでのインターバル処理をやってみる
    {
        //インターバルにカウントを加算
        _shootIntervalTimer += deltaTime * _speed.CurrentSpeed;

        if (isShoot && _shootInterval < _shootIntervalTimer)
        {
            //eventを実行
            OnBulletShoot?.Invoke();
            _audio.Play();

            //場合に応じた弾を生成
            NormalBulletController bulletController = _normalBulletPool.Pool.Get();

            GameObject bullet = bulletController.gameObject;
            bullet.transform.position = _muzzle.position;

            Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
            // Rayを撃ち、当たっていたらその座標に向ける
            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            {             
                bullet.transform.forward = hit.point - _muzzle.transform.position;
            }
            //当たっていなければ、Rayの終着点に向かって撃つ
            else
            {
                bullet.transform.forward = Camera.main.transform.forward * _rayLength - _muzzle.transform.position;
            }

            //弾を動かす
            bullet.GetComponent<NormalBulletController>().MoveStart(_normalBulletPool, _soundEffectPool);

            //インターバルをリセット
            _shootIntervalTimer = 0f;
        }
    }
}
