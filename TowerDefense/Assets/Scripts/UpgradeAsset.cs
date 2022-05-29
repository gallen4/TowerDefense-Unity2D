using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{

    [CreateAssetMenu]

    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Sprite sprite;

        [Header("Параметры")]
        public int[] costByLevel = {3};
    }
}