using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FoxTail.Serenade.Experimental.Dialogue.Testing {
    public class LineReader : MonoBehaviour {
        [SerializeField] private TextAsset inkJSON;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private GameObject[] dialogueChoseButtons;
        private TextMeshProUGUI[] dialogueChose;
        private Story currentStory;

        private void Start() {
            currentStory = new Story(inkJSON.text);

            dialogueChose = new TextMeshProUGUI[dialogueChoseButtons.Length];
            for (int index = 0; index < dialogueChoseButtons.Length; index++) {
                dialogueChose[index] = dialogueChoseButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.D) && currentStory.canContinue) {
                dialogueText.text = currentStory.Continue();;

                if (currentStory.currentChoices.Count != 0) {
                    EventSystem.current.SetSelectedGameObject(dialogueChoseButtons[0].gameObject);
                    for (int index = 0; index < currentStory.currentChoices.Count; index++) {
                        dialogueChoseButtons[index].gameObject.SetActive(true);
                        dialogueChose[index].text = currentStory.currentChoices[index].text;
                    }
                }
            }


            if (currentStory.currentChoices.Count == 0) {
                for (int index = 0; index < dialogueChoseButtons.Length; index++) {
                    dialogueChoseButtons[index].gameObject.SetActive(false);
                }  
            }
        }

        public void HandleClicked(int choiceIndex) {
            currentStory.ChooseChoiceIndex(choiceIndex);
            dialogueText.text = currentStory.Continue();
            if (currentStory.currentChoices.Count != 0) {
                    EventSystem.current.SetSelectedGameObject(dialogueChoseButtons[0].gameObject);
                    for (int index = 0; index < currentStory.currentChoices.Count; index++) {
                        dialogueChoseButtons[index].gameObject.SetActive(true);
                        dialogueChose[index].text = currentStory.currentChoices[index].text;
                    }
                }
        }
    }
}
