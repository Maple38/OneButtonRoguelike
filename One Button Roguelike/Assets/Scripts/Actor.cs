using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Actor : Entity
    {
        private GridCell _currentCell;

        protected override void OccupyCell(GridCell cell)
        {
            base.OccupyCell(cell);
            _currentCell.GrabPickup(this);
        }

        protected override void SetCell(GridCell cell)
        {
            if (_currentCell != null)
            {
                _currentCell.Vacate(this);
            }

            cell.Register(this);
            _currentCell = cell;
        }

        protected void TryMove(Vector2Int moveVector)
        {
            GridCell targetCell;
            try
            {
                targetCell = ParentGrid.GetCell(_currentCell.Coordinates + moveVector);
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