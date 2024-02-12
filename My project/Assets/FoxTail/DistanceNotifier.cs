using System;
using UnityEngine;

namespace FoxTail.Unlinked
{
    public class DistanceNotifier
    {
        public event Action OnNotify;

        private Vector3 referencePosition;
        private float distance;
        private float squaredDistance;

        private bool checkInside;
        private bool enable;

        public void Init(Vector3 referencePosition, float distance, bool checkInside = false, bool triggerContinously = false) {
            this.referencePosition = referencePosition;
            this.distance = distance;
            this.checkInside = checkInside;

            squaredDistance = distance * distance;

            enable = true;

            if (!triggerContinously) OnNotify += Disable;
        }

        public void Disable() {
            enable = false;

            OnNotify -= Disable;
        }

        public void Tick(Vector3 position) {
            if (!enable) return; 

            var currentSqaredDistance = (referencePosition - position).sqrMagnitude;

            if (checkInside) {
                CheckInside(currentSqaredDistance);
            }
            else {
                CheckOutside(currentSqaredDistance);
            }
        }

        private void CheckInside(float distance) {
            if (distance <= squaredDistance) {
                OnNotify?.Invoke();
            }
        }

        private void CheckOutside(float distance) {
            if (distance >= squaredDistance) {
                OnNotify?.Invoke();
            }
        }
    }
}