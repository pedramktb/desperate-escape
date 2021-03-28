using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class TimeEngine : MonoBehaviour
    {
        List<Timer> timers;

        #region Unity Functions
        private void Awake()
        {
            timers = new List<Timer>();
        }

        private void Update()
        {
            if (timers.Count == 0)
                return;
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                if (Time.time > timers[i].executeTime)
                {
                    timers[0].action.Invoke();
                    timers.RemoveAt(i);
                }
            }
            //if (timers[0].remainingTime < 0)
            //{
            //    timers[0].action.Invoke();
            //    timers.RemoveAt(0);
            //    timers.Sort(Timer.CompareByDuration);
            //}
        }
        #endregion


        #region Public Functions
        public void StartTimer(Timer timer)
        {
            timers.Add(timer);
            //timers.Sort(Timer.CompareByDuration);
        }

        public void KillTimer(Timer timer)
        {
            timers.Remove(timers.Find(x => x.ID == timer.ID));
        }
        #endregion
    }
}