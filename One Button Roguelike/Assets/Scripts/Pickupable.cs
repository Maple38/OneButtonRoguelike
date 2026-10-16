using DefaultNamespace;

public abstract class Pickupable : Entity
{
    public abstract void OnPickup(Actor actor);
}
