using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utility
{
    public class Timer
    {
        public Action action;
        public string ID;
        public float executeTime;
        public Timer(float timeToExecute, string ID, Action function)
        {
            this.executeTime = Time.time + timeToExecute;
            action = function;
            this.ID = ID;
        }

        public static int CompareByDuration(Timer t1, Timer t2)
        {
            if (t1.executeTime == t2.executeTime) return 0;

            if (t1.executeTime < t2.executeTime) return 1;

            return -1;
        }
    }
}
