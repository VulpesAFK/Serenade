using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.Screenplay.Template {
    public class ScriptDialogue : MonoBehaviour {
        [SerializeField] protected TMP_Text canvasText => GameObject.Find("Main Text").GetComponent<TMP_Text>();

        protected bool isTextFinished;
        public void ResetIsTextFinished() => isTextFinished = false;

        protected bool interactiveInput;
        
        protected virtual void Update() {
            interactiveInput = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
