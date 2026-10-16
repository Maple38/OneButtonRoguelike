using DefaultNamespace;

public abstract class Pickupable : Entity
{
    public abstract void OnPickup(Actor actor);
    protected override void SetCell(GridCell cell) => cell.Register(this);
}
