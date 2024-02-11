using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticle;

        private Stats stats;
        private ParticleManager particleManager;

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();

        }
        public void Damage(float amount)
        {
            Debug.Log($"{core.transform.parent.name} has been damaged");
            stats.Health.Decrease(amount);
            particleManager.StartParticlesWithRandomRotation(damageParticle);
        }

    }
}
