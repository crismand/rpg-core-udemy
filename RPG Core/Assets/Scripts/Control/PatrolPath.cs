using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float WaypointGizmoRadius = 0.3f;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), WaypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }
        }

        public int GetNextIndex(int i)
        {
            if(i == transform.childCount - 1)
                return 0;
            return (i + 1);
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}

