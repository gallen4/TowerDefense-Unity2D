using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
using System;

namespace TowerDefense
{


    public class Level : MonoBehaviour
    {
        
        [SerializeField] private Episode m_Episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resultStars;

        public bool isCompleted => gameObject.activeSelf && resultPanel.gameObject.activeSelf;
        public void LoadLevel()
        {
            if (m_Episode)
            {
                LevelSequenceController.Instance.StartEpisode(m_Episode);
            }
        }

        public int Initialise()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_Episode);
            resultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; ++i)
            {
                resultStars[i].color = Color.white;
            }
            return score;
        }
    }
}


