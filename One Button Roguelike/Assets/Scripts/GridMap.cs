using System;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private GridCell[][] _cells; // Internal array of cells
    private Vector2 _gridDisplayOffset = new Vector2(0.5f, 0.5f);

    void Awake()
    {
        if (_cells == null)
        {
            BuildGrid();
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
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _cells[x][y] = new GridCell(x, y, TerrainType.Floor);
            }
        }
    }

    public Vector2 CellWorldPosition(Vector2Int pos)
    {
        return pos + (Vector2)transform.position;
    }

    public GridCell GetCell(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > width || pos.y > height)
        {
            throw new ArgumentOutOfRangeException();
        }
        return _cells[pos.x][pos.y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; 
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Gizmos.DrawSphere(new Vector2(x, y) + _gridDisplayOffset, 0.1f);
            }
        }
    }
}
