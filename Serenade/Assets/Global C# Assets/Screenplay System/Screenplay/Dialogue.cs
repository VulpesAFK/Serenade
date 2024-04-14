using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.Screenplay.Template;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.Screenplay.System {
    public class Dialogue : ScriptDialogue {
        private String empty;
        private bool textFinished = true;
        private IEnumerator textEffect;
        [SerializeField] private ScriptDialogueData dialogueData;

        private int textCounter_;
        private bool readFinished;
        protected override void Update()
        {
            base.Update();

            if (interactiveInput && textFinished) {
                // textCounter_ = textCounter_ > dialogueData.Scripts.Length? 0 : textCounter_;

                if (textCounter_ >= dialogueData.Scripts.Length) {
                    canvasText.text = "";
                    textCounter_ = 0;
                }
                else {
                    textEffect = textTypeEffect(dialogueData.Scripts[textCounter_].Text);
                    StartCoroutine(textEffect);
                    textCounter_++;
                }
            }
        }

        IEnumerator textTypeEffect(String textType) {
            empty = "";
            textFinished = false;

            for (int i = 0; i < textType.Length; i++) {
                empty = empty + textType[i];
                canvasText.text = empty;
                yield return new WaitForSeconds(0.05f);
            }

            textFinished = true;
        }
    }
}
