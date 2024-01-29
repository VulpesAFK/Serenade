using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Unlinked
{
    public class Timer
    {
        private float duration;
        private float startTime;
        private float targetTime;
        public event Action onTimerDone;
        private bool isActive;

        public Timer(float duration) {
            this.duration = duration;
        }

        public void StartTime() {
            startTime = Time.time;
            targetTime = startTime + duration;
            isActive = true;
        }

        public void StopTimer() {
            isActive = false;
        }

        public void Tick() {
            if (!isActive) return; 

            if (Time.time >= targetTime) {
                onTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}
