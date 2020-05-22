using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _currentAction;
        
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            _currentAction?.Cancel();

            _currentAction = action;
            
        }
        
    }
}

