using UnityEngine;

namespace FoxTail {
    public interface IObjectPoolItem {
        void SetObjectPool<TYPE>(ObjectPool pool, TYPE comp) where TYPE : Component;

        void Release();
    }
}