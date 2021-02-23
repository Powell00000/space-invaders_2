using UnityEngine;

namespace Game.Gameplay
{
    public class UnitDefinitionsProvider : MonoBehaviour
    {
        [SerializeField] private UnitDefinition player = null;
        [SerializeField] private UnitDefinition basicEnemy = null;

        public UnitDefinition Player => player;
        public UnitDefinition BasicEnemy => basicEnemy;
    }
}