using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// Unity InputSystem rebinding code. Origininally from Trenchmouth (Ludum Dare 49)
// **THIS CODE MAY BE NON-FUNCTIONAL IN THIS STATE**
// Code edited and documented without testing in-engine
//
// Originally attached to a gameobject with a BTBButton script. 
// This is intended to be used on an object that is disabled when not on a rebind screen.
//
// Bear in mind, this was for a 48 hour jam, and was made to be functional, not clean or portable.

namespace BTB {
    public class BindingButton : MonoBehaviour
    {

        public Text BindName;
        public Text CurrentBinding;
        private InputAction focusedInputAction;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;
        public string BoundAction;
		
        public C3i C; // Command, Control, and Intelligence - a hacky way to avoid static members
			// This also provides hooks into all other objects and instances
        public BTBButton BB; // A custom button controller script

        
        // Start is called before the first frame update
        void Start()
        {
            C = GameObject.Find("C3i").GetComponent<C3i>(); // Find and connect C3i - Seriously, use a static class
            BB = this.gameObject.GetComponent<BTBButton>();
        }

        // Possibly not required
        void Update()
        {
            CurrentBinding.text = focusedInputAction.GetBindingDisplayString(); // Sets current UI Text on attached Text object
        }

        public void StartRebind()
        {
            Debug.Log("Rebind on " + BoundAction);
            focusedInputAction.Disable(); //Disables input in question
            var rebind = focusedInputAction.PerformInteractiveRebinding(); // Basically the reference implementation
            rebind.OnComplete(operation =>
           {
               Debug.Log($"Rebound '{BoundAction}' to '{operation.selectedControl}'");
               operation.Dispose();
           }
            );
            rebind.Start(); // Perform actual rebind operation
            focusedInputAction.Enable(); // Reenable input
            UpdateDisplay();
            BB.RestoreButtonGraphic();
            
        }

        public void Init()
        {
            if(C == null) // Failsafe for timing issues to avoid null reference exceptions
            { C = GameObject.Find("C3i").GetComponent<C3i>(); } // Just make a static class
            focusedInputAction = C.InputAsset.FindAction(BoundAction); // InputAsset an InputActionAsset
			// https://docs.unity3d.com/Packages/com.unity.inputsystem@0.9/manual/ActionAssets.html
            UpdateDisplay();
        }
		
        public void UpdateDisplay() // Does not actually update display - updates the text fields to be displayed.
        {
            BindName.text = BoundAction;
            CurrentBinding.text = focusedInputAction.GetBindingDisplayString();
            
        }
    }
}