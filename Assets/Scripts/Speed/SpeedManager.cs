using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager
{
    private static SpeedManager _instance = new();
    public static SpeedManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private SpeedManager() { }

    /// <summary>
    /// 処理を行うSpeedクラスのList
    /// </summary>
    private readonly List<Speed> _speedList = new();

    /// <summary>
    /// Speedクラスの登録
    /// </summary>
    public void Subscribe(Speed speed)
    {
        _speedList.Add(speed);
    }

    /// <summary>
    /// Speedクラスの登録解除
    /// </summary>
    public void Unsubscribe(Speed speed)
    {
        _speedList.Remove(speed);
    }

    public void ChangeSpeed(float value)
    {
        foreach (var speed in _speedList)
        {
            speed.ChangeSpeed(value);
        }
    }
}
