using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    //no pooling, we die like real men
    public class PlayerController
    {
        [Inject] private UnitDefinitionsProvider unitDefProvider = null;
        [Inject] private PlayableArea playableArea = null;

        private int playerHealth = 0;
        private Player spawnedPlayer;

        public System.Action OnPlayerDied;
        public System.Action OnPlayerHit;

        public Vector3 PlayerPosition => spawnedPlayer.transform.position;
        public int LivesLeft => playerHealth;

        public void MaintainPlayerSpawn()
        {
            DespawnPlayer();
            spawnedPlayer = Object.Instantiate(unitDefProvider.Player.Prefab) as Player;
            playerHealth = unitDefProvider.Player.Stats.HitsTillDeath;
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
            playerHealth = spawnedPlayer.CurrentHealth;
            OnPlayerHit?.Invoke();
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