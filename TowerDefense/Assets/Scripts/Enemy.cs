using SpaceShooter;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDController))]

    public class Enemy : MonoBehaviour
    {
        public enum ArmorType
        {
            Physic = 0,
            Magic = 1
        }
        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power , TDProjectile.DamageType type, int armor) => 
            {// Physic armor
                if(TDProjectile.DamageType.Physic == type)
                    armor = armor / 2;
                return Mathf.Max(power - armor, 1);
            },
            (int power , TDProjectile.DamageType type, int armor) =>
            {// Magic armor
                switch(type)
                {
                    case TDProjectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            }
        };

        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;
        [SerializeField] private int m_Armor = 1;
        [SerializeField] private ArmorType m_ArmorType;

        private Destructible destructible;

        public event Action OnEnd;

        private void Awake()
        {
            destructible = GetComponent<Destructible>();
        }

        private void OnDestroy()
        {
            OnEnd?.Invoke();
            
        }
        public void Use(EnemyAsset enemyAsset)
        {

            var m_Sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            m_Sprite.color = enemyAsset.m_Color;
            m_Sprite.transform.localScale = new Vector3(enemyAsset.m_Scale.x, enemyAsset.m_Scale.y, 1);
            m_Sprite.GetComponent<Animator>().runtimeAnimatorController = enemyAsset.m_Animations;

            GetComponent<SpaceShip>().Use(enemyAsset);
            GetComponentInChildren<CircleCollider2D>().radius = enemyAsset.m_Collider;

            m_Damage = enemyAsset.damage;
            m_Armor = enemyAsset.m_Armor;
            m_ArmorType = enemyAsset.armorType;
            m_Gold = enemyAsset.gold;
        }
        public void onEndPath()
        {
            TDPlayer.Instance.ChangeLife(m_Damage);
        }
        public void onGiveGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            destructible.ApplyDamage(ArmorDamageFunctions[(int) m_ArmorType](damage, damageType, m_Armor));
        }

    }
 #if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }

        }
    }
#endif

}
