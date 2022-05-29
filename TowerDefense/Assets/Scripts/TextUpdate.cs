using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using SpaceShooter;

namespace TowerDefense
{


    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Life }
        public UpdateSource source = UpdateSource.Gold;
        private Text m_Text;

        void Start()
        {
            m_Text = GetComponent<Text>();
            switch (source)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
            }
           
        }

        private void UpdateText(int m_Money)
        {
            m_Text.text = m_Money.ToString();
        }

    }
}
