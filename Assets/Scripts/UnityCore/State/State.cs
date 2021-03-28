using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore.StateMachine
{
    public abstract class State
    {
        public abstract void Init();
        public abstract void DeInit();
        public virtual void Update() { }
    }
}