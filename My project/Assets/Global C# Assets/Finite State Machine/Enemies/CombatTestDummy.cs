using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    private Animator anim;
    [SerializeField] private GameObject hitParticle;

    public void Damage(float amount)
    {
        Debug.Log($"{amount} Damage taken");

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Destroy(gameObject);
    }

    private void Awake() {
        anim = GetComponent<Animator>();

    }
}
