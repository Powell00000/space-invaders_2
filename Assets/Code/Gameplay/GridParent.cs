using UnityEngine;

namespace Game.Gameplay
{
    //parent object for Grids for easiness of creation
    //also translates positions
    public class GridParent : MonoBehaviour
    {
        [Zenject.Inject] private GameplayController gameplayController;
        [Zenject.Inject] private PlayableArea playableArea = null;

        [SerializeField] private GridArea[] grids;
        private Bounds parentBounds;
        private int rowMovementDirection = 1;

        private float maxXPosOffset => playableArea.Width / 2;

        public GridArea[] Grids
        {
            get
            {
                //proofcheck
                if (grids == null || grids.Length == 0)
                {
                    GetGrids();
                }

                return grids;
            }
        }

        //for runtime optimalization
        [ContextMenu("Get grids")]
        private void GetGrids()
        {
            grids = GetComponentsInChildren<GridArea>();
        }

        private void Start()
        {
            parentBounds = new Bounds(transform.position, Vector3.zero);
            for (int i = 0; i < grids.Length; i++)
            {
                parentBounds.Encapsulate(grids[i].GridBounds);
            }
        }

        private void Update()
        {
            if (gameplayController.CurrentGameplayState != EGameplayState.Playing)
            {
                return;
            }
            //we encapsulated all bounds, so now we need to move from left to right and keep inside game view
            var offsetedXPosition = transform.position.x + rowMovementDirection * (parentBounds.size.x / 2);

            if (offsetedXPosition >= maxXPosOffset)
            {
                rowMovementDirection = -1;
                BoundsHit();
            }

            if (offsetedXPosition <= -maxXPosOffset)
            {
                rowMovementDirection = 1;
                BoundsHit();
            }

            transform.position += Vector3.right * rowMovementDirection * Time.deltaTime * 2; //AHA! floating forgotten conts! should be in stats as speed
        }

        private void BoundsHit()
        {
            transform.position += Vector3.down * 1f;
        }
    }
}