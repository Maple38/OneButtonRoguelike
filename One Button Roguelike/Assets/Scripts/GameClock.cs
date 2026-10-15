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
    

    void Awake()
    {
        _tickWheel = new List<ITickAgent>[wheelSize];
        _tickInterval = bpm / 60f;
    }
    void Update()
    {
        _tickTimer += Time.deltaTime;
        while (_tickTimer >= _tickInterval)
        {
            _tickTimer -= _tickInterval;
            DoTick();
        }
    }

    void DoTick()
    {
        List<ITickAgent> moveQueue = _tickWheel[_currentTick];
        foreach (var target in moveQueue)
        {
            int nextDelay = target.OnTick();
            if (nextDelay > 0)
            {
                ScheduleTick(target, nextDelay);
            }
        }
        moveQueue.Clear();
        _currentTick++;
        if (_currentTick >= wheelSize - 1)
        {
            _currentTick = 0;
        }
    }

    public void ScheduleTick(ITickAgent target, int delay)
    {
        _tickWheel[(_currentTick + delay) % wheelSize].Add(target);
    }
}
