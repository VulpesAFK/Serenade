using System;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.ScreenplaySystem.Template {

    [CreateAssetMenu(fileName = "NewScript", menuName = "Script System/Script Data")]
    public class ScreenplayData : ScriptableObject {
        public Script[] Scripts;
    }


}

namespace FoxTail.Serenade.Experimental.ScreenplaySystem.Template {
    [Serializable]
    public class Script {
        public string Text;
    }
}
