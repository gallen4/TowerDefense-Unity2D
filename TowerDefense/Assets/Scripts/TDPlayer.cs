using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpaceShooter;

namespace TowerDefense
{


    public class TDPlayer : Player
    {
        public static new TDPlayer Instance 
        {
            get { return Player.Instance as TDPlayer; } 
        }

        private event Action<int> OnGoldUpdate;
        public event Action<int> OnLifeUpdate;
        public void GoldUpdateSubscribe(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_Gold);
        }
        public void LifeUpdateSubscribe(Action<int> action)
        {
            OnLifeUpdate += action;
            action(Instance.NumLives);
        }

        [SerializeField] private UpgradeAsset healthUpgrade;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
        }


        [SerializeField] private int m_Gold = 0;
        public int Gold => m_Gold;
        public void ChangeGold(int m_GoldChange)
        {
            m_Gold += m_GoldChange;
            OnGoldUpdate(m_Gold);

        }
        public void ChangeLife(int m_LifeChange)
        {
            TakeDamage(m_LifeChange);
            OnLifeUpdate(NumLives);
        }

        [SerializeField] private Tower towerPrefab;
        public void TryBuild(TowerAsset m_TowerAsset, Transform buildSite)
        {
            ChangeGold(-m_TowerAsset.goldCost);
            var tower = Instantiate(towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(m_TowerAsset);
            Destroy(buildSite.gameObject);
        }
    }
}

