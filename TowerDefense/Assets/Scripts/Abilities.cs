using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace TowerDefense
{

    public class Abilities : MonoSingleton<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            private int m_Cost;
            [SerializeField] private int m_Damage = 10;
            [SerializeField] private float m_Radius = 5;
            [SerializeField] private float m_Cooldown = 25f;
            public void Use()
            {
                m_Cost = Abilities.Instance.m_CostUpgrade;
                TDPlayer.Instance.ChangeGold(-m_Cost);

                if(Upgrades.GetUpgradeLevel(Abilities.Instance.fireUpgrade) >= 2)
                {
                    m_Radius = 7;
                }

                if (m_Cost > TDPlayer.Instance.Gold)
                {
                    Instance.m_FireButton.interactable = false;
                }

                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var enemycollider in Physics2D.OverlapCircleAll(position, m_Radius))
                    {
                        print($"Radius : {m_Radius}");
                        if (enemycollider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
            }
        }

        [Serializable]
        public class SlowAbility
        {
            [SerializeField] private int m_Cost = 8;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5f;
            public void Use()
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }
                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var spaceship in FindObjectsOfType<SpaceShip>())
                        spaceship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                IEnumerator SlowAbilityButton()
                {
                    Instance.m_SlowButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_SlowButton.interactable = true;
                }

                foreach (var spaceship in FindObjectsOfType<SpaceShip>())
                    spaceship.HalfMaxLinearVelocity();
                EnemyWaveManager.OnEnemySpawn += Slow;

                Instance.StartCoroutine(Restore());
                Instance.StartCoroutine(SlowAbilityButton());
                
            }
        }

        private void Start()
        {
            for (int i = 0; i < m_Costs.Length; i++)
            {
                m_Costs[i].text = m_CostUpgrade.ToString();
            }

            if (requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(fireUpgrade) && m_CostUpgrade <= TDPlayer.Instance.Gold)
            {
                m_FireButton.interactable = true;
                // TODO: убрать отсюда и сделать другую проверку.
            }
            else
            {
                m_FireButton.interactable = false;
            }
        }
   
        [SerializeField] private Button m_FireButton;
        [SerializeField] private Button m_SlowButton;

        [SerializeField] private Text[] m_Costs;
        [SerializeField] private int m_CostUpgrade;
        [SerializeField] private Image m_TargetingCircle;

        [SerializeField] private FireAbility m_FireAbility;
        [SerializeField] private SlowAbility m_SlowAbility;

        [SerializeField] private UpgradeAsset fireUpgrade;
        [SerializeField] private int requiredUpgradeLevel;

        public void UseFireAbility() => m_FireAbility.Use();
        public void UseSlowAbility() => m_SlowAbility.Use();

    }
}
