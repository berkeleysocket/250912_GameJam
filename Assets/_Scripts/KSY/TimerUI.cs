using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    private InGameLifeTime _timer;
    private TMP_Text _timeText;

    private int minute;
    private int second;

    void Awake()
    {
        _timer = GetComponent<InGameLifeTime>();
        _timeText = GetComponent<TMP_Text>();

        _timer.MinuteCount.OnChangedValue += (count)=> 
        {
            minute = count;
            _timeText.text = $"{minute:00} : {second:00}";


        };
        _timer.SecondCount.OnChangedValue += (count)=> 
        {
            second = count;
            _timeText.text = $"{minute:00} : {second:00}";
        };
    }

    void Start()
    {
        _timer.StartTimer();
    }
}
