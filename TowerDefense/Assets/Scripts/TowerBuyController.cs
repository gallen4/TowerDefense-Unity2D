using System.Collections;
using System.Collections.Generic;
using SpaceShooter;
using UnityEngine.UI;
using UnityEngine;

namespace TowerDefense
{


    public class TowerBuyController : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_TowerAsset;
        public void SetTowerAsset(TowerAsset asset)
        {
            m_TowerAsset = asset;
        }
        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform m_BuildSite;
        public void SetBuildSite(Transform value)
        {
            m_BuildSite = value;
        }

        private void Start()
        {
            TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_TowerAsset.goldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_TowerAsset.TowerGUI;
        }

        private void GoldStatusCheck(int Gold)
        {
            if(Gold > m_TowerAsset.goldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }    
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
            BuildSite.HideControls();
        }

    }
}
