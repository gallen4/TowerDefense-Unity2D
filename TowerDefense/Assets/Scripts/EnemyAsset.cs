using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
 
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        public Color m_Color = Color.white;
        public Vector2 m_Scale = new Vector2(3, 3);
        public RuntimeAnimatorController m_Animations;

        [Header("Параметры")]
        public float m_MoveSpeed = 1; 
        public int m_HP = 1;
        public int m_Armor = 0;
        public Enemy.ArmorType armorType;
        public int m_Score = 1;
        public float m_Collider = 0.19f;
        public int damage = 1;
        public int gold = 1;
    }
}