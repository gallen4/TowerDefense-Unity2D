using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    
    [RequireComponent(typeof(Level))]

    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Text m_neededStarsText;
        [SerializeField] private Level m_rootLevel;
        [SerializeField] private int m_neededStars = 3;

        public bool RootIsActive => m_rootLevel.isCompleted;

        public void TryActivate()
        {
            gameObject.SetActive(m_rootLevel.isCompleted);
            
            if(m_neededStars > MapCompletion.Instance.TotalScore)
            {
                m_neededStarsText.text = m_neededStars.ToString();
            }
            else
            {
                m_neededStarsText.transform.parent.gameObject.SetActive(false);
                GetComponent<Level>().Initialise();
            }
        }
    }
}