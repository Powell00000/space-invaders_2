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

        [SerializeField, Tooltip("Used for additional points. Time elapsed is not clamped (can receive 120%)")]
        private int maxGameTimeInSeconds = 60;

        [SerializeField]
        private int basePointsAmountForFinishing = 1000;

        public float DownwardOffset => downwardOffset;
        public float SecondsToSpawnSpecialShip => secondsToSpawnSpecialShip;
        public int MaxGameTimeInSeconds => maxGameTimeInSeconds;
        public int BasePointsAmountForFinishing => basePointsAmountForFinishing;
    }
}
