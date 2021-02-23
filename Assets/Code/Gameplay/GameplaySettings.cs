using UnityEngine;

namespace Assets.Code.Gameplay
{
    [CreateAssetMenu(menuName = "Settings/Gameplay")]
    public class GameplaySettings : ScriptableObject
    {
        [SerializeField, Tooltip("Distance for enemies to move downward upon reaching gameplay bounds")]
        private float downwardOffset = 0.2f;

        [SerializeField]
        private float secondsToSpawnSpecialShip = 4f;

        [SerializeField]
        private int maxGameTimeInSeconds = 60;

        public float DownwardOffset => downwardOffset;
        public float SecondsToSpawnSpecialShip => secondsToSpawnSpecialShip;
    }
}
