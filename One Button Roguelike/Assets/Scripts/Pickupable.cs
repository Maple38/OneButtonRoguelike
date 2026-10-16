using DefaultNamespace;
using UnityEngine;

public abstract class Pickupable : MonoBehaviour
{
    public abstract void OnPickup(Actor actor);
}
