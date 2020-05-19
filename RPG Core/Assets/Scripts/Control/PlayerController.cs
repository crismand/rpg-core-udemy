using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera mainCamera;

        private Ray _lastRay;

        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();
            if (_mover == null) Debug.LogError("PlayerController cannot find Mover");

            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hitWorld = Physics.Raycast(ray, out hit);

            if (hitWorld)
            {
                _mover.MoveTo(hit.point);
            }
        }
    }
}


