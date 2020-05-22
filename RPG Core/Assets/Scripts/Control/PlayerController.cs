using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using Movement;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;
        private Camera mainCamera;

        private Ray _lastRay;

        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();
            if (_mover == null) Debug.LogError("PlayerController cannot find Mover");
            _fighter = GetComponent<Fighter>();
            if(_fighter == null) Debug.LogError("No Fighter component on player");

            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            Debug.Log("Nothing to do!");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!_fighter.CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    _fighter.Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hitWorld = Physics.Raycast(GetRay(), out hit);

            if (hitWorld)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMovementAction(hit.point);
                }

                return true;
            }
            return false;
        }

        private void MoveWithinRange(Transform target)
        {
            
        }
        

        private Ray GetRay()
        {
            return mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}


