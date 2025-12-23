using System;
using UnityEngine;

public class InGameLifeTime : MonoBehaviour
{
    [SerializeField] private float startMinute = 1;
    [SerializeField] private float startSecond = 1;

    private float _currentMinute = 0f;
    private float _currentSecond = 0f;

    private float _minuteCount = 0f;
    private float _secondCount = 0f;

    public bool IsActive {get; private set;} = false;
    public bool IsEnd {get; private set;} = false;

    public event Action OnTimmer;
    public event Action OnEnd;

    public void StartTimer()
    {
        IsActive = true;
        _currentMinute = startMinute * 60f;
        _currentSecond = startSecond;

        OnTimmer?.Invoke();
    }

    void Update()
    {
        if(!IsActive) return;

        if(_currentSecond > 0)
        {
            _currentSecond -= Time.deltaTime;
            return;  
        }

        if(_currentMinute > 0)
        {
            _currentMinute -= Time.deltaTime;
            return;
        }

        if(!IsEnd && IsActive)
        {
            IsEnd = true;
            IsActive = false;

            OnEnd?.Invoke();
        }
    }
}
