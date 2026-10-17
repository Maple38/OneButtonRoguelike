using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField] private int bpm;
    public static GameClock Instance;
    [SerializeField] private int wheelSize;
    private List<ITickAgent>[] _tickWheel;
    private int _currentTick;
    private float _tickInterval;
    private float _tickTimer;
    private bool _timerActive;


    void Awake()
    {
        Instance = this;
        _tickInterval = bpm > 0 ? 60f / bpm : 1f;
        _tickWheel = new List<ITickAgent>[wheelSize];
        for (int i = 0; i < wheelSize; i++)
        {
            _tickWheel[i] = new List<ITickAgent>();
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
    void Update()
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

    void DoTick()
    {
        var moveQueue = new List<ITickAgent>(_tickWheel[_currentTick]);
        _tickWheel[_currentTick].Clear();
            foreach (var target in moveQueue)
            {
                int nextDelay = target.OnTick();
                if (nextDelay > 0)
                {
                    ScheduleTick(target, nextDelay);
                }
            }

            _currentTick = (_currentTick + 1) % wheelSize;
    }

    public void ScheduleTick(ITickAgent target, int delay)
    {
        _tickWheel[(_currentTick + delay) % wheelSize].Add(target);
    }
}