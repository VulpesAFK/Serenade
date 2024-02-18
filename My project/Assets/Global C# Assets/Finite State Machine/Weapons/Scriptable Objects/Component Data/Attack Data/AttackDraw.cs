using System;
using UnityEngine;

namespace FoxTail {
    [Serializable]
    public class AttackDraw : AttackData
    {
        /* 
            * Allows the user to adjust how the curve progresses over time
            * Kept within the between 0 and 1
        */
        [field: SerializeField] public AnimationCurve DrawCurve { get; private set; }

        /*
            * The total time it requires to draw the bow
            * If you want to calculate the frame it is 1 / animation sample rate * number of frames
        */
        [field: SerializeField] public float DrawTime { get; private set; }
    }
}