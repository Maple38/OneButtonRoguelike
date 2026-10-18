using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public static GameClock Instance;
    [SerializeField] private int bpm;
    [SerializeField] private int wheelSize;
    private int _currentTick;
    private float _tickInterval;
    private float _tickTimer;
    private List<ITickAgent>[] _tickWheel;
    private bool _timerActive;


    private void Awake()
    {
        Instance = this;
        _tickInterval = bpm > 0 ? 60f / bpm : 1f;
        _tickWheel = new List<ITickAgent>[wheelSize];
        for (var i = 0; i < wheelSize; i++)
        {
            _tickWheel[i] = new List<ITickAgent>();
        }
    }

    private void Update()
    {
        if (_timerActive)
        {
            _tickTimer += Time.deltaTime;
            while (_tickTimer >= _tickInterval)
            {
                _tickTimer -= _tickInterval;
                DoTick();
            }
        }
    }

    public void ActivateTimer()
    {
        if (!_timerActive)
        {
            _timerActive = true;
            _currentTick++;
            DoTick();
        }
    }

    private void DoTick()
    {
        var moveQueue = new List<ITickAgent>(_tickWheel[_currentTick]);
        _tickWheel[_currentTick].Clear();
        foreach (var target in moveQueue)
        {
            var nextDelay = target.OnTick(_currentTick) ?? 0;
            if (nextDelay > 0)
            {
                ScheduleDelay(target, nextDelay);
            }
        }

        _currentTick = (_currentTick + 1) % wheelSize;
    }

    public void ScheduleDelay(ITickAgent target, int delay)
    {
        ScheduleOnTick(target, (_currentTick + delay) % wheelSize);
    }

    public void ScheduleOnTick(ITickAgent target, int tickNumber)
    {
        _tickWheel[tickNumber].Add(target);
    }
}