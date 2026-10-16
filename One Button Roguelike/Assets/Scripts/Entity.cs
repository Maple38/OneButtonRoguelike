using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Vector2Int spawnCoordinates;
    [SerializeField] private GridMap startingGrid;
    protected GridMap ParentGrid;


    protected void Start()
    {
        if (ParentGrid == null)
        {
            Init(startingGrid, spawnCoordinates);
        }
    }

    protected void OccupyCell(GridCell cell)
    {
        cell.Occupy(this);
        transform.position = ParentGrid.CellWorldPosition(cell.Coordinates);
    }

    public void Init(GridMap parentGrid, Vector2Int cellCoordinate)
    {
        if (ParentGrid == null)
        {
            ParentGrid = parentGrid;
            MoveToCell(ParentGrid.GetCell(cellCoordinate));
        }
    }

    protected virtual void MoveToCell(GridCell cell) => OccupyCell(cell);
}