using System;
using Ksy.Utility;
using UnityEngine;

public class InGameLifeTime : MonoBehaviour
{
    [SerializeField] private float maxMinute = 1;
    [SerializeField] private float maxSecond = 1;

    private float _currentMinute = 0f;
    private float _currentSecond = 0f;

    public NotifyValue<int> minuteCount {get; private set;} = new NotifyValue<int>();

    public bool IsActive {get; private set;} = false;
    public bool IsEnd {get; private set;} = false;

    public event Action OnTimmer;
    public event Action OnEnd;

    public void StartTimer()
    {
        IsActive = true;

        OnTimmer?.Invoke();
    }

    void Update()
    {
        if(!IsActive || IsEnd) return;

        if(_currentSecond <= maxSecond)
        {
            _currentSecond += Time.deltaTime;
            return;
        }

        if(_currentMinute / 60 >= maxMinute)
        {
            _currentMinute += Time.deltaTime;
            //_minuteCount = 
        }
    }
}
