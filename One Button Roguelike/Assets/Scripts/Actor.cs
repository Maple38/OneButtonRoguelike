using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private Vector2Int spawnCoordinates;
        [SerializeField] private GridMap startingGrid;
        private GridCell _currentCell;
        private GridMap _parentGrid;

        protected virtual void Start()
        {
            if (_parentGrid == null)
            {
                Init(startingGrid, spawnCoordinates);
            }
        }

        public void Init(GridMap parentGrid, Vector2Int cellCoordinate)
        {
            if (_parentGrid == null)
            {
                _parentGrid = parentGrid;
                OccupyCell(_parentGrid.GetCell(cellCoordinate));
            }
        }

        protected void OccupyCell(GridCell newCell)
        {
            if (_currentCell != null)
            {
                _currentCell.CellActor = null;
            }

            _currentCell = newCell;
            _currentCell.Occupy(this);
            transform.position = _parentGrid.CellWorldPosition(_currentCell.Coordinates);

            _currentCell.GrabPickup(this);
        }

        protected void TryMove(Vector2Int moveVector)
        {
            GridCell targetCell;
            try
            {
                targetCell = _parentGrid.GetCell(_currentCell.Coordinates + moveVector);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

            if (targetCell.CanEnter())
            {
                OccupyCell(targetCell);
            }
        }
    }
}