using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState attackState;

    protected int attackCounter;

    protected virtual void Start()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCounter", attackCounter % 3);
        weaponAnimator.SetInteger("attackCounter", attackCounter % 3);
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);   

        attackCounter++;

        gameObject.SetActive(false);
    }

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
    }

    public void InitializeWeapon(PlayerAttackState attackState)
    {
        this.attackState = attackState;
    }
}
