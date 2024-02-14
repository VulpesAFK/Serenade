using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Unlinked
{
    // Takes a starting position and a desired distance in which met will invoke an event 
    public class DistanceNotifier
    {
        public event Action OnNotify;

        private Vector3 referencePosition;
        private float distance;
        private float squareDistance;

        private bool checkInside;
        private bool enabled;

        public void InIt(Vector3 referencePosition, float distance, bool checkInside = false, bool triggerContinuously = false) {
            this.referencePosition = referencePosition;
            this.distance = distance;

            squareDistance = distance * distance;

            this.checkInside = checkInside;

            enabled = true;

            if (!triggerContinuously) {
                OnNotify += Disable;
            }
        }

        public void Disable() {
            enabled = false;
            OnNotify -= Disable;
        }

        public void Tick(Vector3 position) {
            if (!enabled) return;

            var currentSquareDistance = (referencePosition - position).sqrMagnitude;

            if (checkInside) {
                CheckInside(currentSquareDistance);
            }
            else {
                CheckOutside(currentSquareDistance);
            }
        }
        private void CheckInside(float dist) {
            if (dist <= squareDistance) {
                OnNotify?.Invoke();
            }
        }

        private void CheckOutside(float dist) {
            if (dist >= squareDistance) {
                OnNotify?.Invoke();
            }
        }
    }
}
