using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    [SerializeField] private Transform target;

    private NavMeshAgent _navMeshAgent;
    private Camera mainCamera;

    private Ray _lastRay;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();                                                    
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
            
        }
        //Debug.DrawRay(_lastRay.origin, _lastRay.direction * 100);
    }

    void MoveToCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hitWorld = Physics.Raycast(ray, out hit);

        if (hitWorld)
        {
            _navMeshAgent.destination = hit.point;
        }
    }
}
