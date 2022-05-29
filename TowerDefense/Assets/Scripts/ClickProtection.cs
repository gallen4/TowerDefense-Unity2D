using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    public class ClickProtection : MonoSingleton<ClickProtection>, IPointerClickHandler
    {
        private Image Blocker;
        private void Start()
        {
            Blocker = GetComponent<Image>();
            Blocker.enabled = false;
        }
        private Action<Vector2> m_OnClickAction;
        public void Activate(Action<Vector2> mouseAction)
        {
            Blocker.enabled = true;
            m_OnClickAction = mouseAction;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Blocker.enabled = false;
            m_OnClickAction(eventData.pressPosition);
            m_OnClickAction = null;
        }
    }
}