using System.Collections;
using System.Collections.Generic;
using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{

    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () => { isCompleted = true; }; 
        }
        public bool IsCompleted => isCompleted;
    }
}
