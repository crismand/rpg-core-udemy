using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using Core;
using Movement;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        [Range(5.0f, 20.0f)] [SerializeField] private float chaseDistance = 5.0f;

        [Range(2.0f, 10.0f)] [SerializeField] private float suspicionTime = 3.0f;

        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float wayPointTolerance = 1.0f;
        [SerializeField] private float wayPointDwellTime = 2.0f;
        
        private GameObject _player;
        private Fighter _fighter;
        private Mover _mover;
        private ActionScheduler _actionScheduler;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeAtWaypoint = Mathf.Infinity;
        private int currentWaypointIndex = 0;

        public void Start()
        {
            _player = GameObject.FindWithTag("Player");
            if(_player == null) Debug.LogError("AIController could not find Player in the scene");

            _fighter = GetComponent<Fighter>();
            if(_fighter == null) Debug.LogError("AIController cannot find Fighter component");

            _mover = GetComponent<Mover>();
            if(_mover == null) Debug.LogError("AIController cannot find Mover component");

            _actionScheduler = GetComponent<ActionScheduler>();
            if (_actionScheduler == null) Debug.LogError("AIController cannot find Action Scheduler component");

            guardPosition = transform.position;
        }

        public void Update()
        {
            if (GetComponent<Health>().IsDead){ return;}
            
            if ((DistanceToPlayer() < chaseDistance) && _fighter.CanAttack(_player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                if (patrolPath != null)
                {
                    PatrolBehavior();
                }
                else
                {
                    GuardBehavior();
                }
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            if (AtWaypoint())
            {
                timeAtWaypoint = 0;
                CycleWaypoint();
            }
            
            if (timeAtWaypoint > wayPointDwellTime)
                _mover.StartMovementAction(patrolPath.GetWaypoint(currentWaypointIndex));
            
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            if (Vector3.Distance(transform.position, patrolPath.GetWaypoint(currentWaypointIndex)) < wayPointTolerance)
            {
                return true;
            }
            return false;
        }

        private void SuspicionBehavior()
        {
            _actionScheduler.CancelCurrentAction();
        }

        private void GuardBehavior()
        {
            _mover.StartMovementAction(guardPosition);
        }

        private void AttackBehavior()
        {
            _fighter.Attack(_player);
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(this.transform.position, _player.transform.position);
        }

        private void OnDrawGizmos()
        {
            //OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

