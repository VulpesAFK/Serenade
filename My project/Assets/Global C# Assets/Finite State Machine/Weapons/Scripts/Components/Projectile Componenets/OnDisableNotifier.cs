using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class OnDisableNotifier : MonoBehaviour
    {
        public event Action OnDisableEvent;

        private void OnDisable() {
            OnDisableEvent?.Invoke();
        }

        [ContextMenu("Test")]
        private void Test() {
            gameObject.SetActive(false);
        }
    }
}
