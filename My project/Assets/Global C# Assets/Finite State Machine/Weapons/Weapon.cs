using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState attackState;

    protected int attackCounter;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.AmountOfAttacks)
        {
            attackCounter = 0;
        }

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
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

    public virtual void AnimationStartMovementTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.MovementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        attackState.SetPlayerVelocity(0);
    }

    public virtual void AnimationStartFlipTrigger()
    {
        attackState.SetFlipCheck(true);
    }
    public virtual void AnimationStopFlipTrigger()
    {
        attackState.SetFlipCheck(false);
    }

    public virtual void AnimationActionTrigger()
    {

    }



    public void InitializeWeapon(PlayerAttackState attackState)
    {
        this.attackState = attackState;
    }
}
