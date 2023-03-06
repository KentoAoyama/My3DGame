using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager
{
    /// <summary>
    /// �������s��Speed�N���X��List
    /// </summary>
    private static List<Speed> _speedList = new();

    private static float _currentSpeed = 1f;
    public static float CurrentSpeed => _currentSpeed;

    /// <summary>
    /// Speed�N���X�̓o�^���s��static���\�b�h
    /// </summary>
    public static void Subscribe(Speed speed)
    {
        _speedList.Add(speed);
    }

    /// <summary>
    /// Speed�N���X�̓o�^�������s��static���\�b�h
    /// </summary>
    public static void Unsubscribe(Speed speed)
    {
        _speedList.Remove(speed);
    }

    /// <summary>
    /// �|�[�Y����
    /// </summary>
    public void Pause()
    {
        ChangeSpeed(0f, ChangeSpeedType.All);
    }

    /// <summary>
    /// �|�[�Y�̉�������
    /// </summary>
    public void Resume()
    {
        ChangeSpeed(1f, ChangeSpeedType.All);
    }

    public void ChangeSpeed(float value, ChangeSpeedType type)
    {
        _currentSpeed = value;

        foreach (var speed in _speedList)
        {
            switch(type)
            {
                case ChangeSpeedType.All:
                    speed.ChangeValue(value);

                    break;
                case ChangeSpeedType.NonPlayer:
                    if (speed.SpeedType != SpeedType.Player)
                        speed.ChangeValue(value);

                    break;
                case ChangeSpeedType.NonUI:
                    if (speed.SpeedType != SpeedType.UI)
                        speed.ChangeValue(value);

                    break;
            }
                
            speed.ChangeValue(value);
        }
    }

    public void Reset()
    {
        _speedList.Clear();
    }
}