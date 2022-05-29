using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;


namespace TowerDefense
{

    public class Tower : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset radiusUpgrade;
        [SerializeField] private float m_Radius = 5.0f;
        [SerializeField] private float m_Lead = 0.1f;
        private Turret[] m_Turret;
        private Rigidbody2D m_Target = null;


       
        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.TowerGraphics;
            m_Turret = GetComponentsInChildren<Turret>();
            foreach(var turret in m_Turret)
            {
                turret.AssignLoadout(asset.turretProperties);
            }
            GetComponentInChildren<BuildSite>().SetAvailableTowers(asset.UpgradesTo);
        }

        private void Awake()
        {
            var level = Upgrades.GetUpgradeLevel(radiusUpgrade);
            m_Radius += (level * 1f);
        }

        private void Update()
        {
            if(m_Target)
            {
                Vector2 targetVector = m_Target.transform.position - transform.position;
                if (Vector3.Distance(m_Target.transform.position, transform.position) <= m_Radius)
                {

                    foreach (var turret in m_Turret)
                    {

                        turret.transform.up = m_Target.transform.position - turret.transform.position + (Vector3)m_Target.velocity * m_Lead;
                        turret.Fire();
                    }
                }
                else
                {
                    m_Target = null;
                }
            }
            else
            {
                var EnemyEnter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (EnemyEnter)
                {
                    m_Target = EnemyEnter.transform.root.GetComponent<Rigidbody2D>();
                }
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}
