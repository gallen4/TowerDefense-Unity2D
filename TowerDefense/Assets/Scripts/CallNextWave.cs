using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace TowerDefense
{

    public class CallNextWave : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;

        private EnemyWaveManager manager;
        private float TimeToNextWave;

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                TimeToNextWave = time;
            };
        }

        public void CallWave()  
        {
            manager.ForceNextWave();
        }

        private void Update()
        {
            var bonus = (int)TimeToNextWave;
            if (bonus < 0)
                bonus = 0;

            TimeToNextWave -= Time.deltaTime;
            bonusAmount.text = bonus.ToString();
        }
    }
}
