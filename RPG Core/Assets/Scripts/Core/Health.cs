using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float _health = 100.0f;
        private bool isDead = false;
        public bool IsDead => isDead;

        private Animator _animator;
        private ActionScheduler _actionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            if(_animator == null) Debug.LogError("Health cannot find animator");

            _actionScheduler = GetComponent<ActionScheduler>();
            if(_actionScheduler == null) Debug.LogError("Health cannot find ActionScheduler");
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(_health - damage, 0.0f);
            Debug.Log(_health.ToString() + " hit points remaining...");

            if (_health == 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            _actionScheduler.CancelCurrentAction();
            _animator.SetTrigger("Death");
        }
    }
}

