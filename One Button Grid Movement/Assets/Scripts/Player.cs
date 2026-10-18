using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor, ITickAgent
{
    [SerializeField] private int partialTurnOnTick;
    [SerializeField] private int fullTurnOnTick;
    [SerializeField] private int partialTurnAngle;
    private InputAction _actionMain;
    private int _cardinalRotation;
    private int _tick;

    protected override void Start()
    {
        base.Start();
        _actionMain = InputSystem.actions.FindAction("Jump");
        GameClock.Instance.ScheduleOnTick(this, partialTurnOnTick);
        GameClock.Instance.ScheduleOnTick(this, fullTurnOnTick);
    }

    private void Update()
    {
        if (_actionMain.WasPressedThisFrame())
        {
            TryMove(Vector2Int.RoundToInt(transform.up));
        }
    }

    public int? OnTick(int tick)
    {
        if (tick == fullTurnOnTick)
        {
            FullTurn();
        }
        else if (tick == partialTurnOnTick)
        {
            PartialTurn();
        }

        return null;
    }

    private void FullTurn()
    {
        _cardinalRotation = (_cardinalRotation + 1) % 4;
        transform.rotation = Quaternion.Euler(0, 0, _cardinalRotation * 90);
        GameClock.Instance.ScheduleOnTick(this, fullTurnOnTick);
    }

    private void PartialTurn()
    {
        transform.Rotate(0, 0, partialTurnAngle);
        GameClock.Instance.ScheduleOnTick(this, partialTurnOnTick);
    }
}