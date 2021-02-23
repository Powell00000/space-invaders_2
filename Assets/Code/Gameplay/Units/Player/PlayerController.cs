using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    //no pooling, we die like real men
    public class PlayerController
    {
        [Inject] private UnitDefinitionsProvider unitDefProvider = null;
        [Inject] private PlayableArea playableArea = null;

        public System.Action OnPlayerDied;

        public Vector3 PlayerPosition => spawnedPlayer.transform.position;

        private Player spawnedPlayer;

        public void MaintainPlayerSpawn()
        {
            DespawnPlayer();
            spawnedPlayer = Object.Instantiate(unitDefProvider.Player.Prefab) as Player;

            spawnedPlayer.transform.position = playableArea.PlayerSpawnPosition;

            ConnectEvents();
        }

        private void ConnectEvents()
        {
            spawnedPlayer.OnDamageReceived += PlayerReceivedDamage;
            spawnedPlayer.OnDeath += PlayerDied;
        }

        private void DisconnectEvents()
        {
            spawnedPlayer.OnDamageReceived -= PlayerReceivedDamage;
            spawnedPlayer.OnDeath -= PlayerDied;
        }

        private void PlayerReceivedDamage()
        {

        }

        private void PlayerDied(Unit deadUnit)
        {
            DespawnPlayer();
            if (OnPlayerDied != null)
            {
                OnPlayerDied();
            }
        }

        public void DespawnPlayer()
        {
            if (spawnedPlayer)
            {
                DisconnectEvents();
                GameObject.Destroy(spawnedPlayer.gameObject);
            }
        }
    }
}