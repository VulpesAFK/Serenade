using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> coreComponents = new List<CoreComponent>();

    public void LogicUpdate() {
        foreach (CoreComponent conponent in coreComponents) conponent.LogicUpdate();
    }

    public void AddComponent(CoreComponent component) {
        if(!coreComponents.Contains(component)) coreComponents.Add(component);
    }

    public TYPE GetCoreComponent <TYPE>() where TYPE:CoreComponent {
        var comp = coreComponents.OfType<TYPE>().FirstOrDefault();
        if (comp) return comp;
        
        comp = GetComponentInChildren<TYPE>();
        if (comp) return comp;
        Debug.LogWarning($"{typeof(TYPE)} not found on {transform.parent.name}");

        return null;
    }
}
