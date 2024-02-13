using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxTail.Unlinked;
using FoxTail;

public class Weapon : MonoBehaviour
{
    public WeaponData Data { get; private set; }
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
    public event Action OnUseInput;

    public event Action<bool> OnCurrentInputChange;

    private int currentAttackCounter;
    private Timer attackCounterResetTimer;
    public Core Core { get; private set; }

    private bool currentInput;
    public bool CurrentInput {
        get => currentInput;
        set
        {
            if (currentInput != value) {
                currentInput = value;
                OnCurrentInputChange?.Invoke(currentInput);
            }
        }
    }

    public WeaponAnimationEventHandler EventHandler { get; private set; }

    // Important settings for required weaponry settings
    # region Awake() & Enter() Functions
    private void Awake() {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("Weapon Sprite").gameObject;

        anim = BaseGameObject.GetComponent<Animator>();
        EventHandler =  BaseGameObject.GetComponent<WeaponAnimationEventHandler>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }
    public void Enter() {
        Debug.Log($"{transform.name} enter");

        attackCounterResetTimer.StopTimer();

        anim.SetBool("active", true);
        anim.SetInteger("counter", CurrentAttackCounter);

        OnEnter?.Invoke();
    }
    # endregion


    public void SetCore(Core core) {
        Core = core;
    }
    public void SetData(WeaponData data) {
        Data = data;
    }
    private void Exit() {
        OnExit?.Invoke();

        CurrentAttackCounter++;

        anim.SetBool("active", false);
        attackCounterResetTimer.StartTime(Time.time);
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
        EventHandler.OnUseInput += HandleUseInput;
        attackCounterResetTimer.onTimerDone += ResetAttackCounter;
    }

    private void OnDisable() {
        EventHandler.OnFinish -= Exit;
        EventHandler.OnUseInput -= HandleUseInput;
        attackCounterResetTimer.onTimerDone -= ResetAttackCounter;
    }
    # endregion

    private void HandleUseInput() => OnUseInput?.Invoke();
}
