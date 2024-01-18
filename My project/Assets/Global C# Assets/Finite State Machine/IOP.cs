using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOP : MonoBehaviour
{
    [SerializeField] private E2 entity;
    private AttackDetails attackDetails;
    // Start is called before the first frame update
    void Start()
    {
        attackDetails.DamageAmount = 4;  
        attackDetails.StunDamageAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        attackDetails.Position = transform.position; 
        if(Input.GetKeyDown(KeyCode.W))
        {
            entity.Damage(attackDetails);
        }
    }
}
