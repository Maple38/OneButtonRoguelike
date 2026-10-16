using System;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private GridCell[,] _cells; // Internal array of cells
    private Vector2 TileWorldOffset => transform.TransformPoint(new Vector2(0.5f, 0.5f));
    private TileRenderer _tileRenderer;

    private void Awake()
    {
        _tileRenderer = GetComponent<TileRenderer>();
        if (_cells == null)
        {
            BuildGrid();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Gizmos.DrawSphere(new Vector2(x, y) + TileWorldOffset, 0.1f);
            }
        }
    }

    public void Init(int gridWidth, int gridHeight)
    {
        width = gridWidth;
        height = gridHeight;
        BuildGrid();
    }

    private void BuildGrid()
    {
        _cells = new GridCell[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                _cells[x, y] = new GridCell(x, y, TerrainType.Floor);
            }
        }

        _tileRenderer.RenderTiles(_cells);
    }

    public Vector2 CellWorldPosition(Vector2Int pos) => pos + TileWorldOffset;

    public GridCell GetCell(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        {
            throw new ArgumentOutOfRangeException();
        }

        return _cells[pos.x, pos.y];
    }
}