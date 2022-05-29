using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;

        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDeath += () => { StopLevelActivity(); LevelResultController.Instance.Show(false); };

            m_ReferenceTime += Time.time;
            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity(); 
                if (m_ReferenceTime <= Time.time)
                {
                    levelScore--;
                }
                MapCompletion.SaveEpisodeResult(levelScore);
            });
            

            void LifeChange(int _)
            {
                levelScore--;
                TDPlayer.Instance.OnLifeUpdate -= LifeChange;
            }
            TDPlayer.Instance.OnLifeUpdate += LifeChange;
        }

        private void StopLevelActivity()
        {
            foreach(var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T: MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<EnemyWave>();
            DisableAll<Tower>();
            DisableAll<Projectile>();
            DisableAll<CallNextWave>();
        }
    }
}
