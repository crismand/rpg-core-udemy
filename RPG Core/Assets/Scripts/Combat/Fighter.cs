using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Health _target;
        private Mover _mover;
        private Animator _animator;

        [Range(0.5f, 2.5f)] [SerializeField] private float timeBetweenAttacks = 1.0f;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private float _weaponDamage = 10.0f;
        
        public void Start()
        {
            _mover = GetComponent<Mover>();
            if(_mover == null) Debug.LogError("Mover not accessible from 'fighter'");
            _animator = GetComponent<Animator>();
            if(_animator == null) Debug.LogError("Fighter cannot get reference to animator on player");
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (!CanAttack()) return;

            if (Vector3.Distance(_target.transform.position, transform.position) > 2.0f)
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Cancel();
                if (_timeSinceLastAttack > timeBetweenAttacks)
                {
                        AttackBehavior();
                }
            }
            
        }

        private bool CanAttack()
        {
            if ((_target == null) || (_target.IsDead)) 
                return false;
            return true;
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            
            Health health = target.GetComponent<Health>();
            return (health != null) && (!health.IsDead);
        }

        private void AttackBehavior()
        {
            this.transform.LookAt(_target.transform);
            if (_timeSinceLastAttack > timeBetweenAttacks)
            {
                _animator.ResetTrigger("StopAttack");
                _animator.SetTrigger("Attack");
                _timeSinceLastAttack = 0.0f;
                //Damage dealt in Hit(), Event triggered by animator
            }
        }
        
        //AnimationEvent
        public void Hit()
        {
            if(_target != null)
                _target.TakeDamage(_weaponDamage);
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            _animator.ResetTrigger("Attack");
            _animator.SetTrigger("StopAttack");
            _target = null;
        }

 
    }
}

