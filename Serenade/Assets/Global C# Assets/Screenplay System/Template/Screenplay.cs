using UnityEngine;
using TMPro;

namespace FoxTail.Serenade.Experimental.ScreenplaySystem.Template {
    public class Screenplay : MonoBehaviour {
        public bool IsScreenplayFinished { get => isScreenplayFinished; set => isScreenplayFinished = value; }
        protected bool isScreenplayFinished;
        protected TMP_Text screenplayText;
        private int screenplayTextLangth;
        private int i = 0;
        [SerializeField] private ScreenplayData screenplayData;

        protected virtual void Start() {
            screenplayText = GameObject.Find("ScreenplayText").GetComponent<TMP_Text>();
            screenplayTextLangth = screenplayData.Scripts.Length;
        }
        public virtual void TEST() { 
            if (i >= screenplayTextLangth) {
                screenplayText.text = "";
                isScreenplayFinished = true;
                i = 0;
            }
            else {
                screenplayText.text = screenplayData.Scripts[i].Text;
                i++;
            }

        }

        public void ResetIsScreenplayFinished() => isScreenplayFinished = false;

    }
}
