using System;
using System.Collections;
using System.Collections.Generic;
using FoxTail.Serenade.Experimental.Screenplay.Template;
using UnityEngine;

namespace FoxTail.Serenade.Experimental.Screenplay.System {
    public class Dialogue : ScriptDialogue {
        private String empty;
        private bool textFinished = true;
        [SerializeField] private ScriptDialogueData dialogueData;
        private IEnumerator textCoroutines;

        private int textCounter_;

        private bool isReading;
        private bool canSkip;

        protected override void Update()
        {
            base.Update();

            if (interactiveInput && !isReading && !canSkip) {

                if (textCounter_ >= dialogueData.Scripts.Length) {
                    canvasText.text = "";
                    textCounter_ = 0;
                    textFinished = false;
                }
                else {
                    textCoroutines = textTypeEffect(dialogueData.Scripts[textCounter_].Text);
                    StartCoroutine(textCoroutines);

                    isReading = true;
                    canSkip = true;
                }
            }
            else if (interactiveInput && isReading && canSkip && !textFinished) {
                StopCoroutine(textCoroutines);
                textCoroutines = null;

                canvasText.text = dialogueData.Scripts[textCounter_].Text;

                isReading = false;
                canSkip = false;
                textFinished = true;
                textCounter_++;
            }   
        }

        IEnumerator textTypeEffect(String textType) {
            empty = "";
            textFinished = false;

            for (int i = 0; i < textType.Length; i++) {
                empty = empty + textType[i];
                canvasText.text = empty;
                yield return new WaitForSeconds(0.02f);
            }

            textFinished = true;
            isReading = false;
            canSkip = false;
            textCounter_++;
        }
    }
}
