using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Actor : Entity
    {
        public GridCell CurrentCell { get; private set; }

        public override void OccupyCell(GridCell cell)
        {
            base.OccupyCell(cell);
            CurrentCell.GrabPickup(this);
        }

        protected override void SetCell(GridCell cell)
        {
            if (CurrentCell != null)
            {
                CurrentCell.Vacate(this);
            }

            cell.Register(this);
            CurrentCell = cell;
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