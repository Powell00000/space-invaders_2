using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.UI
{
    internal class LeaderboardsView : MonoBehaviour
    {
        [Zenject.Inject] private LeaderboardController leaderboardCtrl;
        [SerializeField] private Transform content;
        [SerializeField] private LeaderboardEntry entryPrefab;

        private List<LeaderboardEntry> spawnedEntries = new List<LeaderboardEntry>();

        private void OnEnable()
        {
            var entries = leaderboardCtrl.GetEntries();

            bool moreEntriesThanSpawned = entries.Length > spawnedEntries.Count;
            int index = 0;
            for (index = 0; index < spawnedEntries.Count; index++)
            {
                if (index < entries.Length)
                {
                    spawnedEntries[index].SetData(index, entries[index]);
                    spawnedEntries[index].gameObject.SetActive(true);
                }
                else
                {
                    if (moreEntriesThanSpawned)
                    {
                        break;
                    }
                    spawnedEntries[index].gameObject.SetActive(false);
                }
            }

            for (int i = index; i < entries.Length; i++)
            {
                if (i < spawnedEntries.Count)
                {
                    spawnedEntries[i].SetData(i, entries[i]);
                }
                else
                {
                    var spawnedEntry = Instantiate(entryPrefab, content);
                    spawnedEntry.SetData(i, entries[i]);
                    spawnedEntries.Add(spawnedEntry);
                }
            }
        }

        private void OnDisable()
        {

        }
    }
}
