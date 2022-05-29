using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
using System.IO;

namespace TowerDefense
{

    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }
        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log($"Episode completed with score {levelScore}");
            }
        }

        [SerializeField] private EpisodeScore[] completionData;
        [SerializeField] private int totalScore;
        public int TotalScore => totalScore;
        //[SerializeField] private EpisodeScore[] branchCompletionData;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
            foreach(var episodeScore in completionData)
            {
                totalScore += episodeScore.score;  
            }
        }
        public int GetEpisodeScore(Episode m_Episode)
        {
            foreach(var data in completionData)
            {
                if (data.episode == m_Episode)
                    return data.score;
                
            }
            return 0;
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if(item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        totalScore += levelScore - item.score;
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, completionData);
                    }
                }
            }

        }
    }
}
