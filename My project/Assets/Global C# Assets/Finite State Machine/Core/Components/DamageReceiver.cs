using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticle;

        private CoreComp<Stats> stats;
        private CoreComp<ParticleManager> particleManager;

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<Stats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
        public void Damage(float amount)
        {
            Debug.Log($"{core.transform.parent.name} has been damaged");
            stats.Component?.DecreaseHealth(amount);
            particleManager.Component?.StartParticlesWithRandomRotation(damageParticle);
        }

    }
}
