using UnityEngine;

namespace FoxTail {
    public class WeaponInputHold : WeaponComponent {
        // Reference to the animator 
        private Animator anim;
        // Reference to the main bool that will hold the instance
        private bool input;

        private bool minHoldPassed;

        # region Awake() & OnDestroy() Functions

        protected override void HandleEnter()
        {
            base.HandleEnter();

            minHoldPassed = false;
        }
        protected override void Awake(){
            base.Awake();

            anim = GetComponentInChildren<Animator>();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
            eventHandler.OnMinHoldPassed += HandleMinHoldPassed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
            eventHandler.OnMinHoldPassed -= HandleMinHoldPassed;

        }
        # endregion

        private void SetAnimationParameter() {
            if (input)
            {
              anim.SetBool("hold", input);
                return;
            }
            if (minHoldPassed)
            {
                anim.SetBool("hold", input);
            }

        }

        private void HandleCurrentInputChange(bool newInput) {
            input = newInput;
            SetAnimationParameter();
        }

        private void HandleMinHoldPassed() {
            minHoldPassed = true;

            SetAnimationParameter();
        }
    }
}