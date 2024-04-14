using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.Screenplay.Temaplate {
    public class ScriptDialogueData : ScriptableObject {
        public Script[] Scripts;
    }

    [Serializable]
    public class Script {
        public string Text;
    }
}
