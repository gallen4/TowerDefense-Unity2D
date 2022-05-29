using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{


    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemySpawn;


        [SerializeField] private Path[] paths;
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private EnemyWave currentWave;
        public event Action OnAllWavesDead;
        private int activeEnemyCount = 0;

        private void EnemyDeath()
        {
            if (--activeEnemyCount == 0)
            {
                ForceNextWave();
            }
        }   
        private void Start()
        {
        
            currentWave.Prepare(SpawnEnemies);
        }

        public void ForceNextWave()
        {
            if (currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if(activeEnemyCount == 0) OnAllWavesDead?.Invoke();
            }
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; ++i)
                    {
                        var e = Instantiate(m_EnemyPrefab, paths[pathIndex].spawnArea.RandomInsideZone, Quaternion.identity);
                        e.OnEnd += EnemyDeath;
                        e.Use(asset);
                        e.GetComponent<TDController>().SetPath(paths[pathIndex]);
                        activeEnemyCount++;
                        OnEnemySpawn?.Invoke(e);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");    
                }
            }
            currentWave = currentWave.prepareNext(SpawnEnemies);
        }
    }
}
