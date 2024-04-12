using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.ScreenplaySystem.Template {

    [CreateAssetMenu(fileName = "NewScript", menuName = "Script System/Script Data")]
    public class ScreenplayData : ScriptableObject {
        public Script[] Scripts;
    }


}
namespace FoxTail.Serenade.Experimental.ScreenplaySystem.Template {
    [System.Serializable]
    public class Script {
        public string Text;
    }
}
