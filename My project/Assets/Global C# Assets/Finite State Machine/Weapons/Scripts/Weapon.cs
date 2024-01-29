using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxTail.Unlinked;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int numberOfAttacks;
    [SerializeField] private float attackCounterResetCooldown;

    // A public properties variable that will return and set a value with called or manipulated
    public int CurrentAttackCounter {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= numberOfAttacks? 0 : value;
    }
    private Animator anim;
    private GameObject baseGameObject;
    public event Action OnExit;

    private int currentAttackCounter;
    private Timer attackCounterResetTimer;

    private WeaponAnimationEventHandler eventHandler;
    public void Enter() {
        print($"{transform.name} enter");

        attackCounterResetTimer.StopTimer();

        anim.SetBool("active", true);
        anim.SetInteger("counter", CurrentAttackCounter);
    }
    private void Exit() {
        OnExit?.Invoke();

        CurrentAttackCounter++;

        anim.SetBool("active", false);
        attackCounterResetTimer.StartTime();
    }

    private void Awake() {
        baseGameObject =  transform.Find("Base").gameObject;
        anim = baseGameObject.GetComponent<Animator>();
        eventHandler =  baseGameObject.GetComponent<WeaponAnimationEventHandler>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    private void Update() {
        attackCounterResetTimer.Tick();
    }

    private void ResetAttackCounter() => CurrentAttackCounter = 0;

    # region Weapon Animation Handler => thpis Event Subscription
    private void OnEnable() {
        eventHandler.OnFinish += Exit;
        attackCounterResetTimer.onTimerDone += ResetAttackCounter;
    }

    private void OnDisable() {
        eventHandler.OnFinish -= Exit;
        attackCounterResetTimer.onTimerDone -= ResetAttackCounter;
    }
    # endregion
}
