using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail {
    public class DialogueTrigger : MonoBehaviour {
        [SerializeField] private GameObject visualCue;
        private Collider2D isPlayerInRange;
        [SerializeField] private TextAsset inkJSON;

        [SerializeField] private LayerMask playerLayerMask;

        private void Awake() {
            visualCue.SetActive(false);
        }

        private void Update() {
            isPlayerInRange = Physics2D.OverlapCircle(transform.position, 3, playerLayerMask);
            if (isPlayerInRange) {
                visualCue.SetActive(true);
                if (Input.GetMouseButtonDown(0)) {
                    Debug.Log($"This is the asset {inkJSON.text}");
                }
            }

            else {
                visualCue.SetActive(false);
            }
        }
    }
}
