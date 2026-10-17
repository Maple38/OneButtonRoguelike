public class Pickupable : Entity
{
    public void OnPickup()
    {
        GameClock.Instance.ActivateTimer();
        GameManager.Instance.Reposition(this);
    }

    protected override void SetCell(GridCell cell) => cell.Register(this);
}