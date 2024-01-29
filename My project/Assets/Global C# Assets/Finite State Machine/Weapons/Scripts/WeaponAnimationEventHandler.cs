using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEventHandler : MonoBehaviour
{
    // A global event that will signal to a signal to any listeners that the attack animation is finished
    public event Action OnFinish;
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
}
