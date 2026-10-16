using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor, ITickAgent
{
    [SerializeField] private int moveInterval;
    private InputAction _actionMain;

    protected override void Start()
    {
        base.Start();
        _actionMain = InputSystem.actions.FindAction("Jump");
        GameClock.Instance.ScheduleTick(this, moveInterval);
    }

    void Update()
    {
        if (_actionMain.WasPressedThisFrame())
        {
            TryMove(Vector2Int.RoundToInt(transform.up));
        }
    }

    public int OnTick()
    {
        transform.Rotate(0, 0, -90);
        return moveInterval;
    }
}
