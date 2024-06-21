using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FoxTail
{
    public class TypeWriter : MonoBehaviour
    {
        private TMP_Text text;
        private string diaplayText = "This text will make a perfect typing effect just like Reverse 1999 - how amazing";

        private void Start() {
            text = gameObject.GetComponent<TMP_Text>();
            text.text = "";
        }

        // Update is called once per frame
        private void Update() {
            if (Input.GetKeyDown(KeyCode.K)) {
                StartCoroutine(Typer());
            }
        }

        private IEnumerator Typer() {
            foreach (char letter  in diaplayText.ToCharArray()) {
                text.text += letter;
                yield return new WaitForEndOfFrame();
            }
        }

    }
}
