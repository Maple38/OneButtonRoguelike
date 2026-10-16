using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(GridMap))]
public class TileRenderer : MonoBehaviour
{
    private Tilemap _tilemap;
    private Tile _tileWall;
    private Tile _tileFloor;

    private void Awake()
    {
        _tilemap = GetComponentInChildren<Tilemap>();
    }

    public void RenderTiles(GridCell[,] cellArray)
    {
        _tilemap.ClearAllTiles();
        foreach (var cell in cellArray)
        {
            var tileToDraw = cell.CellTerrain switch
            {
                TerrainType.Floor => _tileFloor,
                TerrainType.Wall => _tileWall,
                _ => throw new ArgumentOutOfRangeException()
            };
            _tilemap.SetTile((Vector3Int)cell.Coordinates, tileToDraw);
        }
    }
}