using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private void AnimationFinishTrigger()
    {
        Destroy(gameObject);
    }
}
