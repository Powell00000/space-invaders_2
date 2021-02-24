using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Code
{
    public class LeaderboardController : IInitializable
    {
        private string filePath;
        private Leaderboard leaderboard;

        public void Initialize()
        {
            filePath = Application.persistentDataPath + "/Leaderboard";
            if (!TryLoadFromDisk())
            {
                leaderboard = new Leaderboard();
            }
        }

        public Leaderboard.Entry[] GetEntries()
        {
            leaderboard.entries.Sort();
            return leaderboard.entries.ToArray();
        }

        public bool TryLoadFromDisk()
        {
            bool fileExisits = File.Exists(filePath);
            if (fileExisits)
            {
                var fileStream = File.OpenRead(filePath);
                BinaryFormatter converter = new BinaryFormatter();
                leaderboard = converter.Deserialize(fileStream) as Leaderboard;

                fileStream.Close();
            }
            return fileExisits;
        }

        public void SaveToDisk()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            var fileStream = File.OpenWrite(filePath);

            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(fileStream, leaderboard);

            fileStream.Close();
        }

        public void AddEntry(Leaderboard.Entry entry)
        {
            leaderboard.AddEntry(entry);
        }

        public int GetBiggestPoints()
        {
            return leaderboard.GetBiggestPoints();
        }
    }

    [System.Serializable]
    public class Leaderboard
    {
        internal List<Entry> entries;

        public Leaderboard()
        {
            entries = new List<Leaderboard.Entry>();
        }

        internal void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        internal int GetBiggestPoints()
        {
            int biggestPoints = -1;
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Points > biggestPoints)
                {
                    biggestPoints = entries[i].Points;
                }
            }
            return biggestPoints;
        }

        [System.Serializable]
        public class Entry : IComparable<Entry>
        {
            public string PlayerName;
            public int Points;

            public int CompareTo(Entry other)
            {
                return other.Points - Points;
            }
        }
    }
}
