using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;
using System;

namespace TowerDefense
{


    public class TDController : AIController
    {
        [SerializeField] private UnityEvent onEndPath;

        private Path m_Path;
        private int pathIndex;

        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            pathIndex = 0;
            SetPatrolBehaviour(m_Path[pathIndex]);
        }

        protected override void GetNewPoint() 
        {
            pathIndex += 1;
            if (m_Path.Length > pathIndex)
            {
                SetPatrolBehaviour(m_Path[pathIndex]);
            }
            else
            {
                onEndPath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}

