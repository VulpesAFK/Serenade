using UnityEngine;

public static class GenericNonImplementedError <TYPE> {
    public static TYPE TryGet(TYPE value, string name) {
        if (value != null) return value;

        Debug.LogError($"!! {typeof(TYPE)} not implemented on {name}");
        return default; 
    }
}
