using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    private GameObject baseGameObject;
    public event Action OnExit;

    private WeaponAnimationEventHandler eventHandler;
    public void Enter() {
        print($"{transform.name} enter");

        anim.SetBool("active", true);
    }
    private void Exit() {
        OnExit?.Invoke();
        anim.SetBool("active", false);
    }

    private void Awake() {
        baseGameObject =  transform.Find("Base").gameObject;
        anim = baseGameObject.GetComponent<Animator>();
        eventHandler =  baseGameObject.GetComponent<WeaponAnimationEventHandler>();
    }

    # region Weapon Animation Handler => thpis Event Subscription
    private void OnEnable() {
        eventHandler.OnFinish += Exit;
    }

    private void OnDisable() {
        eventHandler.OnFinish -= Exit;
    }
    # endregion
}
