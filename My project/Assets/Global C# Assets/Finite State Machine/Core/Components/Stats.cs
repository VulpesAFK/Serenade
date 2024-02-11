using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail;
using UnityEngine;

public class Stats : CoreComponent
{
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Poise { get; private set; }

    [SerializeField] private float poiseRecoveryRate;
    protected override void Awake()
    {
        base.Awake();

        Health.InIt();
        Poise.InIt();
    }

    private void Update() {
        if (Poise.CurrentValue.Equals(Poise.MaxValue)) {
            return;
        }

        Poise.Increase(poiseRecoveryRate * Time.deltaTime);
    }
}
