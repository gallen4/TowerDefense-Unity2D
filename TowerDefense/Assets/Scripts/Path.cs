using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{


    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea m_spawnArea;
        public CircleArea spawnArea => m_spawnArea;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.grey;

            foreach(var point in m_Points)
                Gizmos.DrawSphere(point.transform.position, point.Radius);
        }

        [SerializeField] private AIPointPatrol[] m_Points;
        public int Length { get => m_Points.Length; } 
        public AIPointPatrol this[int i] { get => m_Points[i]; }

    }
}