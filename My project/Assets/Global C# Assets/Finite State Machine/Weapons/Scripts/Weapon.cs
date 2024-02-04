using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxTail.Unlinked;
using FoxTail;

public class Weapon : MonoBehaviour
{
    [field: SerializeField]public WeaponData Data { get; private set; }
    [SerializeField] private float attackCounterResetCooldown;

    // A public properties variable that will return and set a value with called or manipulated
    public int CurrentAttackCounter {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks? 0 : value;
    }
    private Animator anim;
    public GameObject BaseGameObject { get; private set; }
    public GameObject WeaponSpriteGameObject { get; private set; }

    public event Action OnExit;
    public event Action OnEnter;

    private int currentAttackCounter;
    private Timer attackCounterResetTimer;
    public Core Core { get; private set; }

    public WeaponAnimationEventHandler EventHandler { get; private set; }
    public void Enter() {
        Debug.Log($"{transform.name} enter");

        attackCounterResetTimer.StopTimer();

        anim.SetBool("active", true);
        anim.SetInteger("counter", CurrentAttackCounter);

        OnEnter?.Invoke();
    }

    public void SetCore(Core core) {
        Core = core;
    }
    private void Exit() {
        OnExit?.Invoke();

        CurrentAttackCounter++;

        anim.SetBool("active", false);
        attackCounterResetTimer.StartTime(Time.time);
    }

    private void Awake() {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("Weapon Sprite").gameObject;

        anim = BaseGameObject.GetComponent<Animator>();
        EventHandler =  BaseGameObject.GetComponent<WeaponAnimationEventHandler>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    # region Attack Counter Logic
    // All in relationship ticking and restarting the attack counter
    private void Update() => attackCounterResetTimer.Tick();
    private void ResetAttackCounter() => CurrentAttackCounter = 0;
    # endregion

    // Trigger to animation event triggers of the animations to the main weaponry scripts this => Event Handler -> Weapon -> Attack State
    // Connect when the animation has finished its single animation
    // Connect the restart counter to make sure the combo resets after some time
    # region OnEnable() & OnDisable() Functions
    private void OnEnable() {
        EventHandler.OnFinish += Exit;
        attackCounterResetTimer.onTimerDone += ResetAttackCounter;
    }

    private void OnDisable() {
        EventHandler.OnFinish -= Exit;
        attackCounterResetTimer.onTimerDone -= ResetAttackCounter;
    }
    # endregion
}
