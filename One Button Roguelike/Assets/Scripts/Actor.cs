using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Actor : Entity
    {
        protected GridCell CurrentCell;

        protected override void MoveToCell(GridCell cell)
        {
            if (CurrentCell != null)
            {
                CurrentCell.CellActor = null;
            }

            CurrentCell = cell;
            OccupyCell(cell);
            CurrentCell.GrabPickup(this);
        }

        protected void TryMove(Vector2Int moveVector)
        {
            GridCell targetCell;
            try
            {
                targetCell = ParentGrid.GetCell(CurrentCell.Coordinates + moveVector);
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