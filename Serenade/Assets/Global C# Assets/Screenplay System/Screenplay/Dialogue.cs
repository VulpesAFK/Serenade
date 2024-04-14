using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.Screenplay.Template;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.Screenplay.System {
    public class Dialogue : ScriptDialogue {
        private String empty;
        private IEnumerator textEffect;
        
        protected override void Update()
        {
            base.Update();

            if (interactiveInput && textEffect == null) {
                textEffect = textTypeEffect("Hello there, this is a test message just to this silly system");
                StartCoroutine(textEffect);
            }
        }

        IEnumerator textTypeEffect(String textType) {
            empty = "";

            for (int i = 0; i < textType.Length; i++) {
                empty = empty + textType[i];
                canvasText.text = empty;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
