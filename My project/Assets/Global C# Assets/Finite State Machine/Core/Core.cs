using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public Collision Collision { get; private set; }

    private void Awake() 
    {
        Movement = GetComponentInChildren<Movement>(); 
        Collision = GetComponentInChildren<Collision>();     
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
