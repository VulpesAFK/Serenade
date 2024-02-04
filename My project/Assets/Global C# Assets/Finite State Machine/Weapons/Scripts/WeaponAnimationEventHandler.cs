using System;
using UnityEngine;

// Script tied to the animator will be used to trigger and tie together the weapon to the weaponry components
public class WeaponAnimationEventHandler : MonoBehaviour
{
    // A global event that will signal to a signal to any listeners that the attack animation is finished
    public event Action OnFinish;
    // A global event that will be used to signal the toggle of the movement weponry component
    public event Action OnStartMovement;
    public event Action OnFinishMovement;

    //TODO - Correct this to allow for multi-frame hitboxing detection
    public event Action OnAttackAction;


    // Signals the end of the weaponry animation frames
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();

    // Signals the start and the end of the movement scriptable
    private void StartMovementTrigger() => OnStartMovement?.Invoke();
    private void StopMovementTrigger() => OnFinishMovement?.Invoke();

    //TODO - Fix this trigger to allow for multi-frame using a start and an end frame event
    private void AttackActionTrigger() => OnAttackAction?.Invoke();
}
