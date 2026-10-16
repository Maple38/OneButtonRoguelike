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
        }
    }
}