using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(GridMap))]
public class TileRenderer : MonoBehaviour
{
    [SerializeField] private TileBase tileWall;
    [SerializeField] private TileBase tileFloor1;
    [SerializeField] private TileBase tileFloor2;

    private Tilemap _tilemap;

    private void Awake()
    {
        LinkTilemap();
    }

    public void RenderTiles(GridCell[,] cellArray)
    {
        LinkTilemap();

        var totalCells = cellArray.Length;
        var positions = new Vector3Int[totalCells];
        var tiles = new TileBase[totalCells];
        var index = 0;
        foreach (var cell in cellArray)
        {
            tiles[index] = cell.CellTerrain switch
            {
                TerrainType.Floor => (cell.Coordinates.x + cell.Coordinates.y) % 2 == 0 ? tileFloor1 : tileFloor2,
                TerrainType.Wall => tileWall,
                _ => throw new ArgumentOutOfRangeException()
            };

            positions[index] = (Vector3Int)cell.Coordinates;
            index++;
        }

        _tilemap.ClearAllTiles();
        _tilemap.SetTiles(positions, tiles);
    }

    private void LinkTilemap()
    {
        if (_tilemap == null)
        {
            _tilemap = GetComponentInChildren<Tilemap>();
        }
    }
}