using UnityEngine;

namespace FoxTail {
    public interface IObjectPoolItem {
        void SetObjectPool<TYPE>(ObjectPool<TYPE> pool) where TYPE : Component;
    }
}