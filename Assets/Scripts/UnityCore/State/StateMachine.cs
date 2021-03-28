using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected bool toggleStateUpdate = true;
        public State CurrentState { get; protected set; }
        protected virtual void Update()
        {
            if(toggleStateUpdate)
                 CurrentState.Update();
        }
    }
}