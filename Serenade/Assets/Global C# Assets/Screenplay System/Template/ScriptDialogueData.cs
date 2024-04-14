using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.Screenplay.Temaplate {
    [CreateAssetMenu(fileName = "newDialogueData", menuName = "Screenplay/DialogueData")]
    public class ScriptDialogueData : ScriptableObject {
        public Script[] Scripts;
    }

    [Serializable]
    public class Script {
        public string Text;
    }
}
