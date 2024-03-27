using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : CoreComponent {
    // Transform container to hold the particle
    private Transform particleContainer;

    protected override void Awake() {
        base.Awake();

        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation) {
        // Return a passed particle instantiation parameters into the function arguments
        return Instantiate(particlePrefab, position, rotation, particleContainer);
    }

    public GameObject StartParticles(GameObject particlePrefab) {
        // Auto set the position and rotation from the core without needing to pass
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }

    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab) {
        // Generate a random rotation for the the target hit
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }
}
