using System;
using UnityEngine;

namespace FoxTail
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;
        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue { get => currentValue; private set {
            currentValue = Mathf.Clamp(value, 0, MaxValue);

            if (CurrentValue <= 0f) OnCurrentValueZero?.Invoke();
        } }
        private float currentValue;

        public void InIt() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;
        public void Decrease(float amount) => CurrentValue -= amount;
    }
}