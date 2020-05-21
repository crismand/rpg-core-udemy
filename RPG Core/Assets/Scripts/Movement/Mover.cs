using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class Mover : MonoBehaviour
    {

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Fighter _fighter;
        

        
    
        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _fighter = GetComponent<Fighter>();

        }

        // Update is called once per frame
        void Update()
        {

            //Debug.DrawRay(_lastRay.origin, _lastRay.direction * 100);
            UpdateAnimator();
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
        }

        public void StartMovementAction(Vector3 destination)
        {
            _fighter.CancelAttack();
            MoveTo(destination);
        }
        
        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = destination;
        }
    
        void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        
            _animator.SetFloat("ForwardSpeed", localVelocity.z);
        }
    }
}

