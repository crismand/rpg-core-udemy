using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour
    {
        private Transform _target;
        private Mover _mover;

        public void Start()
        {
            _mover = GetComponent<Mover>();
            if(_mover == null) Debug.LogError("Mover not accessible from 'fighter'");
        }

        private void Update()
        {
            if (_target != null)
            {
                if (Vector3.Distance(_target.position, transform.position) > 2.15f)
                {
                    _mover.MoveTo(_target.position);
                }
                else
                {
                    _mover.Stop();
                }
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
            Debug.Log("Take that!");
        }

        public void CancelAttack()
        {
            _target = null;
        }
    }
}

