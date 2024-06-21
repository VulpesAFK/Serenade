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

        /*
        private string InkSpeaker = "speaker";
        private string InkPosition = "position";
        private Dictionary<string, string> InkDictionaryTag = new Dictionary<string, string>();
        private Dictionary<string, Sprite> DialogueSprites = new Dictionary<string, Sprite>();
        private void Awake() {
            InkDictionaryTag.Add(InkSpeaker, "");
            InkDictionaryTag.Add(InkPosition, "");
        }
        */
        private void Start() {
            currentStory = new Story(inkJSON.text);

            dialogueChose = new TextMeshProUGUI[dialogueChoseButtons.Length];
            for (int index = 0; index < dialogueChoseButtons.Length; index++) {
                dialogueChose[index] = dialogueChoseButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.D) && currentStory.canContinue) {
                DisplayText();

                // // * Tag information
                // for (int index = 0; index < currentStory.currentTags.Count; index++) {
                //     string[] inkTag = currentStory.currentTags[index].Split(':');
                //     print($"{inkTag[0]} & {inkTag[1]}");
                // }


            }


            HideButtons();
        }

        private void HideButtons() {
            if (currentStory.currentChoices.Count == 0) {
                for (int index = 0; index < dialogueChoseButtons.Length; index++) {
                    dialogueChoseButtons[index].gameObject.SetActive(false);
                }  
            }
        }

        private void DisplayText() {
            dialogueText.text = currentStory.Continue();

            if (currentStory.currentChoices.Count != 0) {
                EventSystem.current.SetSelectedGameObject(dialogueChoseButtons[0].gameObject);
                for (int index = 0; index < currentStory.currentChoices.Count; index++) {
                    dialogueChoseButtons[index].gameObject.SetActive(true);
                    dialogueChose[index].text = currentStory.currentChoices[index].text;
                }
            }
        }

        private void HandleClicked(int choiceIndex) {
            currentStory.ChooseChoiceIndex(choiceIndex);
            DisplayText();
        }
    }
}
