using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


    public class Handle : XRBaseInteractable
    {
        public Transform rotatingObject = null;
        
        private Vector3 previousHandlePosition;
        private Quaternion previousObjectRotation;
        private IXRSelectInteractor m_handController;

        void Start()
        {
            previousHandlePosition = transform.position;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            m_handController = interactorsSelecting[0];
            previousHandlePosition = transform.position;
            previousObjectRotation = rotatingObject.rotation;
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            m_handController = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);
            
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (!isSelected)
                {
                    return;
                }
                
                if (rotatingObject != null)
                {
                    RotateAroundAnchor(); 
                }
            }
        }

        private void RotateAroundAnchor()
        {
            // Calculate the rotation wrt the controller
            var position = rotatingObject.position;
            Vector3 previousDirection = previousHandlePosition - position;
            Vector3 currentDirection = m_handController.transform.position - position;

            Quaternion rotationToApply = Quaternion.FromToRotation(previousDirection, currentDirection);
            rotatingObject.rotation = rotationToApply * previousObjectRotation;
            
            CorrectRotation();
        }
        
        // Correct the rotation around y-axis
        private void CorrectRotation()
        {
            // trying to correct rotation while preserving pitch, will fix later
            // Vector3 currentUp = rotatingObject.up;
            // Quaternion correctiveRotation = Quaternion.FromToRotation(currentUp, Vector3.up);
            // rotatingObject.rotation = correctiveRotation * rotatingObject.rotation;
        }
    }
    