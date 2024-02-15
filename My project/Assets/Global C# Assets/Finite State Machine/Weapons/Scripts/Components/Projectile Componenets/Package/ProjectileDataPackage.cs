using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    /*
        * Projectile component is a component that does not come from the prefab connected but through the weapon spawning it
        * IE. Many bows will have the same arrow prefab but different damage amounts so damage amount is sent  when spawned

        * Acts as the base component for another components so that the weapon will be able to send specific data through all projectile types
    */
    public abstract class ProjectileDataPackage { }
}
