using System;
using Ksy.Scripts.StressSystem;
using Ksy.Utility;
using UnityEngine;

public class InGameLifeTime : MonoBehaviour
{
    [SerializeField] private float maxMinute = 1;
    [SerializeField] private float maxSecond = 1;

    private float _maxTime = 0f; 
    private float _currentTime = 0f;

    private bool _stressIncrease01 = false;
    private bool _stressIncrease02 = false;
    private bool _stressIncrease03 = false;
    private bool _stressIncrease04 = false;
    private bool _stressIncrease05 = false;


    public NotifyValue<int> SecondCount {get; private set;} = new NotifyValue<int>();
    public NotifyValue<int> MinuteCount {get; private set;} = new NotifyValue<int>();

    public bool IsActive {get; private set;} = false;
    public bool IsEnd {get; private set;} = false;

    public event Action OnActive;
    public event Action OnEnd;

    public void StartTimer()
    {
        _currentTime = 0f;
        _maxTime = maxSecond + maxMinute * 60;

        IsActive = true;
        OnActive?.Invoke();
    }

    void Update()
    {
        if(!IsActive || IsEnd) return;

        StressUp();
        if(_maxTime >= _currentTime)
        {
            _currentTime += Time.deltaTime;

            SecondCount.Value = (int)(_currentTime % 60);
            MinuteCount.Value = (int)(_currentTime / 60f);
            return;
        }

        IsEnd = true;
        IsActive = false;
    }

    private void StressUp()
    {
        if(!_stressIncrease01 && SecondCount.Value >= 1)
        {
            _stressIncrease01 = true;
            StressManager.Instance?.IncreaseStress(1);

        }
        if(!_stressIncrease02 && SecondCount.Value >= 2)
        {
            _stressIncrease02 = true;
            StressManager.Instance?.IncreaseStress(1);

        }
        if(!_stressIncrease03 && SecondCount.Value >= 3)
        {
            _stressIncrease03 = true;
            StressManager.Instance?.IncreaseStress(1);

        }
        if(!_stressIncrease04 && SecondCount.Value >= 4)
        {
            _stressIncrease04 = true;
            StressManager.Instance?.IncreaseStress(1);

        }
        if(!_stressIncrease05 && SecondCount.Value >= 5)
        {
            _stressIncrease05 = true;
            StressManager.Instance?.IncreaseStress(1);

        }
    }
}
