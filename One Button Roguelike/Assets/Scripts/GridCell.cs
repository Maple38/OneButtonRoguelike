using System;
using DefaultNamespace;
using UnityEngine;

public enum TerrainType
{
    Floor,
    Wall,
    // Pit,
}

public class GridCell
{
    public TerrainType CellTerrain;
    public Actor CellActor;
    public Pickupable CellPickup;

    public bool HasActor => CellActor != null;
    public bool IsWalkable => CellTerrain switch
    {
        TerrainType.Floor => true,
        TerrainType.Wall => false,
        _ => throw new ArgumentOutOfRangeException()
    };
    public bool CanEnter() => IsWalkable && !HasActor;

    public Vector2Int Coordinates { get; }

    public GridCell(int x, int y, TerrainType terrain)
    {
        CellTerrain = terrain;
        Coordinates = new Vector2Int(x, y);
    }

}