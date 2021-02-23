using UnityEngine;

namespace Game.Gameplay
{
    //here we have logic for Grid and Cells for enemies to occupy and move with
    public class GridArea : MonoBehaviour
    {
        [SerializeField, Range(1, 20)] private float rowWidth = 10;
        //how many enemies there will be in a row
        [SerializeField, Range(1, 12)] private int cellsCount = 4;
        [SerializeField] private BoxCollider2D boxCol2D = null;

        [SerializeField, Tooltip("Override default enemy unit stats here.")] private EnemyStats overrideEnemyStats;

        [Zenject.Inject] private GameplayController gameplay = null;
        private Bounds gridBounds;

        private float GridSizeX => boxCol2D.size.x * transform.localScale.x;
        private float CellSize => GridSizeX / cellsCount;
        //offset for positioning next Cell
        private Vector3 VectorOffset => transform.right * CellSize;
        private Cell[] cells;

        public Bounds GridBounds => gridBounds;
        public int CellsCount => cellsCount;
        public Cell[] Cells => cells;
        public EnemyStats OverrideEnemyStats => overrideEnemyStats;

        public void Initialize()
        {
            gameplay.OnLevelRestarting += ClearCells;
            gridBounds = new Bounds(transform.position, Vector3.zero);
            BakeCells();
        }

        private void BakeCells()
        {
            cells = new Cell[CellsCount];
            Vector3 cellPosition = transform.position - transform.right * GridSizeX / 2 + VectorOffset / 2;

            //TODO: optimization and memory management
            for (int i = 0; i < cellsCount; i++)
            {
                //for enemies to have something to follow, also better bugchecking in editor
                GameObject cellTransform = new GameObject("Cell");
                cellTransform.transform.SetParent(this.transform, false);
                cellTransform.transform.position = cellPosition;

                cells[i] = new Cell(cellTransform.transform);
                cellPosition += VectorOffset;

                gridBounds.Encapsulate(cellTransform.transform.position);
            }
        }

        public void ClearCells()
        {
            foreach (var child in transform.GetComponentsInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
            cells = null;
        }

        public bool TryGetFreeCell(out Cell freeCell)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].IsOccupied == false)
                {
                    freeCell = cells[i];
                    return true;
                }
            }
            freeCell = null;
            return false;
        }


        public void OnDestroy()
        {
            if (gameplay)
            {
                gameplay.OnLevelRestarting -= ClearCells;
            }
        }

        public class Cell
        {
            private Transform worldTransform;
            private Unit occupant;

            public Transform WorldTransform => worldTransform;
            public Unit Occupant => occupant;

            public bool IsOccupied
            {
                get
                {
                    if (occupant == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            public Cell(Transform worldTransform)
            {
                this.worldTransform = worldTransform;
                occupant = null;
            }

            public void OccupyCell(Unit occupant)
            {
                this.occupant = occupant;
                this.occupant.OnDeath += FreeCell;
            }

            private void FreeCell(Unit deadUnit)
            {
                occupant = null;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 centerPoint = transform.position - transform.right * GridSizeX / 2 + VectorOffset / 2;
            for (int i = 0; i < cellsCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(centerPoint, new Vector2(CellSize, 1));
                centerPoint += VectorOffset;
            }
        }

        private void OnValidate()
        {
            transform.localScale = Vector3.one;
            if (boxCol2D)
            {
                boxCol2D.size = new Vector2(rowWidth, 1);
            }
        }
#endif
    }
}