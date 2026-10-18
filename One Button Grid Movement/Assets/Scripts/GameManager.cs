using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Player player;
    [SerializeField] private GridMap mainGrid;
    [SerializeField] private float minObjectiveDistance;

    private void Awake()
    {
        Instance = this;
    }

    public void Reposition(Pickupable subject)
    {
        var pos = RandomGridPosition(mainGrid);
        while (GridMap.StepsBetween(pos, player.CurrentCell.Coordinates) < minObjectiveDistance)
        {
            pos = RandomGridPosition(mainGrid);
        }

        subject.OccupyCell(mainGrid.GetCell(pos));
    }

    private Vector2Int RandomGridPosition(GridMap grid) =>
        new(Random.Range(0, grid.width), Random.Range(0, grid.height));
}