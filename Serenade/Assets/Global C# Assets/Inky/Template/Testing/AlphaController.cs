using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FoxTail
{
    public class AlphaController : MonoBehaviour
    {
        // private string TextToDisplay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        // private TMP_Text TMPText;
        // private void Start() {
        //     TMPText = GetComponent<TMP_Text>();
        //     TMPText.text = TextToDisplay;


        //     StartCoroutine(BasicTyper());
        // }
        // private IEnumerator BasicTyper() {
        //     for (int i = 0; i < TextToDisplay.Length; i++)
        //     {
        //         StartCoroutine(AlphaChanger(i));   
        //         yield return new WaitForEndOfFrame();
        //     }
        // }

        // private IEnumerator AlphaChanger(int index) {
        //     for (float iteration = 0; iteration <= 1; iteration += 0.2f) {
        //         string alpha = ((int)(iteration * 255)).ToString("X2");
        //         TMPText.text = TMPText.text.Substring(0, index) + $"<alpha=#{alpha}>{TextToDisplay[index]}"; 
        //         yield return new WaitForEndOfFrame();
        //     }
        // }

        private TMP_Text textMeshPro => GetComponent<TMP_Text>();
        private string display = "Hello there";

        private void Start()
        {
            if (textMeshPro != null)
            {
                // Set the initial text
                textMeshPro.text = display;

                // Change the alpha of the second letter to 0.5 (50% transparency)
                StartCoroutine(TypeChanger());
            }
        }

        private IEnumerator TypeChanger() {
            for (int index = 1; index < display.Length; index++)
            {
                ChangeAlphaOfCharacter(index, 0.1f);  
                yield return new WaitForEndOfFrame();
            }
        }

        public void ChangeAlphaOfCharacter(int index, float alpha)
        {
            // Get the text info from the TextMesh Pro component
            // textMeshPro.ForceMeshUpdate();
            TMP_TextInfo textInfo = textMeshPro.textInfo;

            if (index < 0 || index >= textInfo.characterCount)
            {
                Debug.LogWarning("Index out of range.");
                return;
            }
            

            // Get the mesh and vertex colors
            TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
            int meshIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;
            byte alphaByte = (byte)(alpha * 255);

            // Change the alpha for each vertex of the character
            for (int i = 0; i < 4; i++)
            {
                vertexColors[vertexIndex + i].a = alphaByte;
            }

            // Update the mesh with the new colors
            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
    }
}
