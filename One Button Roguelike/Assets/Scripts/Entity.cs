using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private Vector2Int spawnCoordinates;
    [SerializeField] private GridMap startingGrid;
    protected GridMap ParentGrid;


    protected virtual void Start()
    {
        if (ParentGrid == null)
        {
            Init(startingGrid, spawnCoordinates);
        }
    }

    public virtual void OccupyCell(GridCell cell)
    {
        SetCell(cell);
        transform.position = ParentGrid.CellWorldPosition(cell.Coordinates);
    }

    protected abstract void SetCell(GridCell cell);

    public void Init(GridMap parentGrid, Vector2Int cellCoordinate)
    {
        if (ParentGrid == null)
        {
            ParentGrid = parentGrid;
            OccupyCell(ParentGrid.GetCell(cellCoordinate));
        }
    }
}