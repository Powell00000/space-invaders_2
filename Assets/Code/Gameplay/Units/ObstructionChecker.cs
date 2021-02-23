using UnityEngine;

namespace Assets.Code.Gameplay.Units
{
    public static class ObstructionChecker
    {
        private static RaycastHit2D[] dummyArray = new RaycastHit2D[3];

        public static bool IsObstructed(Vector2 origin, Vector2 direction, LayerMask layerMask, float distance)
        {
            return Physics2D.RaycastNonAlloc(origin, direction, dummyArray, distance, layerMask) > 1;
        }
    }
}
