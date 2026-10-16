using System;
using DefaultNamespace;
using UnityEngine;

public enum TerrainType
{
    Floor,
    Wall
    // Pit,
}

public class GridCell
{
    public readonly TerrainType CellTerrain;
    public Actor CellActor { get; private set; }
    public Pickupable CellPickup { get; private set; }

    public GridCell(int x, int y, TerrainType terrain)
    {
        CellTerrain = terrain;
        Coordinates = new Vector2Int(x, y);
    }

    public bool HasActor => CellActor != null;
    public bool HasPickup => CellPickup != null;

    public bool IsWalkable => CellTerrain switch
    {
        TerrainType.Floor => true,
        TerrainType.Wall => false,
        _ => throw new ArgumentOutOfRangeException()
    };

    public Vector2Int Coordinates { get; }
    public bool CanEnter() => IsWalkable && !HasActor;

    public void Register(Pickupable pickupable)
    {
        CellPickup = pickupable;
    }

    public void Register(Actor actor)
    {
        CellActor = actor;
    }

    public void Vacate(Actor actor)
    {
        if (CellActor == actor)
        {
            CellActor = null;
        }
    }

    public void ClearPickup()
    {
        CellPickup = null;
    }

    public void GrabPickup(Actor actor)
    {
        if (CellPickup != null)
        {
            CellPickup.OnPickup(actor);
        }

        ClearPickup();
    }
}