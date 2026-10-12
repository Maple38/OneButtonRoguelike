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
    public Actor CellActor;
    public Pickupable CellPickup;

    public GridCell(int x, int y, TerrainType terrain)
    {
        CellTerrain = terrain;
        Coordinates = new Vector2Int(x, y);
    }

    public bool HasActor => CellActor != null;
    public bool IsWalkable => CellTerrain switch
    {
        TerrainType.Floor => true,
        TerrainType.Wall => false,
        _ => throw new ArgumentOutOfRangeException()
    };

    public Vector2Int Coordinates { get; }
    public bool CanEnter() => IsWalkable && !HasActor;

    public void Occupy(Actor newOccupant)
    {
        CellActor = newOccupant;
    }
}