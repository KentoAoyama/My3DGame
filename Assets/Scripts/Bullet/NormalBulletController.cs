using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UniRx;
using System;

public class NormalBulletController : MonoBehaviour
{
    [Tooltip("弾のスピード")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [Tooltip("弾が破棄されるまでの時間")]
    [SerializeField]
    private float _destroyInterval = 10f;

    [SerializeField]
    private float _time = 0.4f;

    [SerializeField]
    private TrailRenderer _trail;

    private readonly Speed _speed = new(SpeedType.Bullet);

    private IObjectPool<NormalBulletController> _bulletPool;

    private Coroutine _delayCoroutine;

    private SoundEffectPool _soundEffectPool;

    private void Update()
    {
        //前に移動させ続ける
        transform.Translate(
            transform.forward * _bulletSpeed * Time.deltaTime * _speed.CurrentSpeed,
            Space.World);
    }

    /// <summary>
    /// 生成時に呼び出すメソッド
    /// </summary>
    public void MoveStart(NormalBulletPool bulletPool, SoundEffectPool soundEffectPool)
    {
        //オブジェクトプールの参照を渡す
        if (_bulletPool == null) _bulletPool = bulletPool.Pool;
        if (_soundEffectPool == null) _soundEffectPool = soundEffectPool;

        //破棄を行うコルーチンを実行
        _delayCoroutine = StartCoroutine(DestoryInterval());

        _speed.SpeedRp
            .First()
            .Subscribe(ChangeSpeed)
            .AddTo(gameObject);
    }

    private void ChangeSpeed(float speed)
    {
        _trail.time = _time / speed;
    }

    private IEnumerator DestoryInterval()
    {
        yield return new WaitForSeconds(_destroyInterval);

        _delayCoroutine = null;
        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        //命中した際の処理が定義されているかチェック
        if (other.transform.root.TryGetComponent(out IHittable hit))
        {
            hit.Hit();
        }

        //ノックバック処理が定義されているかチェック
        if (other.transform.root.TryGetComponent(out IKnockBackable knockBackable))
        {
            //SoundEffectを生成する
            SoundEffect soundEffect = _soundEffectPool.Pool.Get();
            soundEffect.Initialize(_soundEffectPool, SoundEffectType.Normal, transform.position);

            knockBackable.KnockBack(other, transform.forward);
        }

        Release();
    }

    private void Release()
    {
        if (_delayCoroutine != null) StopCoroutine(_delayCoroutine);

        _trail.Clear();

        _bulletPool.Release(this);
    }
}
