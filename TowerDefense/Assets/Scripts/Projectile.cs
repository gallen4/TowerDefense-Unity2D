﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        public void SetFromOtherProjectile(Projectile other)
        {
            other.GetData(out m_Velocity, out m_Lifetime, out m_Damage, out m_ImpactEffectPrefab);
        }

        private void GetData(out float m_Velocity, out float m_Lifetime, out int m_Damage, out ImpactEffect m_ImpactEffectPrefab)
        {
            m_Velocity = this.m_Velocity; m_Lifetime = this.m_Lifetime; m_Damage = this.m_Damage; m_ImpactEffectPrefab = this.m_ImpactEffectPrefab;
        }







        /// <summary>
        /// Линейная скорость полета снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_Lifetime;

        /// <summary>
        /// Повреждения наносимые снарядом.
        /// </summary>
        [SerializeField] protected int m_Damage;

        /// <summary>
        /// Эффект попадания от что то твердое. 
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                OnHit(hit);
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        protected virtual void OnHit(RaycastHit2D hit)
        {
            var destructible = hit.collider.transform.root.GetComponent<Destructible>();

            if (destructible != null && destructible != m_Parent)
            {
                destructible.ApplyDamage(m_Damage);
            }
        }

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 pos)
        {
            if (m_ImpactEffectPrefab != null)
            {
                var impact = Instantiate(m_ImpactEffectPrefab.gameObject);
                impact.transform.position = pos;
            }

            Destroy(gameObject);
        }


        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

    }
}
#if UNITY_EDITOR
namespace TowerDefense
{
    [CustomEditor(typeof(SpaceShooter.Projectile))]
    public class ProjectileInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create TD projectile"))
            {
                var target = this.target as SpaceShooter.Projectile;
                var tdproj = target.gameObject.AddComponent<TDProjectile>();
                tdproj.SetFromOtherProjectile(target);
            }
        }

    }
}
#endif

