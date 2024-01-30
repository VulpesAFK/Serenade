using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Unlinked
{
    public class Timer
    {
        private float duration; 
        private float targetTime;

        public event Action onTimerDone;
        
        private bool isActive;

        // Constructor
        public Timer(float duration) => this.duration = duration;

        // Start time initiation
        public void StartTime(float startTime) {
            // Global start time to submit as the target  time
            targetTime = startTime + duration;
            // Prevent a timer repeat
            isActive = true;
        }

        public void Tick() {
            // Prevent a cycle of repeating calls
            if (!isActive) return; 

            // Invoke all subscribers and stop after single call
            if (Time.time >= targetTime) {
                // Invoke and stop
                onTimerDone?.Invoke();
                StopTimer();
            }
        }
        public void StopTimer() => isActive = false;
    }
}
