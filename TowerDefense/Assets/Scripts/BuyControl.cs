using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyController m_TowerBuyPrefab;
        private List<TowerBuyController> m_activeControl;
        private RectTransform m_GUITransform;
        private void Awake()
        {
            m_GUITransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;

        }

        private void MoveToBuildSite(BuildSite buildsite)
        {
            if (buildsite)
            {
                var position = Camera.main.WorldToScreenPoint(buildsite.transform.root.position);
                m_GUITransform.anchoredPosition = position;
                m_activeControl = new List<TowerBuyController>();
                foreach (var asset in buildsite.availableTowers)
                {
                    if (asset.IsAble())
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_activeControl.Add(newControl);
                        newControl.SetTowerAsset(asset);

                    }
                }
                if (m_activeControl.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / m_activeControl.Count;
                    for (int i = 0; i < m_activeControl.Count; ++i)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 110);
                        m_activeControl[i].transform.position += offset;
                    }
                    foreach (var TBC in GetComponentsInChildren<TowerBuyController>())
                    {
                        TBC.SetBuildSite(buildsite.transform.root);
                    }
                }

            }
            else
            {
                if (m_activeControl != null)
                {
                    foreach (var control in m_activeControl) Destroy(control.gameObject);
                    m_activeControl.Clear();
                    gameObject.SetActive(false);
                }
            }

        }
    }
}
