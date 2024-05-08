using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


    public class Handle : XRBaseInteractable
    {
        public Transform rotatingObject = null;
        
        private Vector3 previousHandlePosition;
        private Quaternion previousObjectRotation;
        private Vector3 previouseObjectPosition;
        private IXRSelectInteractor m_handController;

        private float powerRatio = 0;

        void Start()
        {
            previousHandlePosition = transform.position;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            m_handController = interactorsSelecting[0];
            previousHandlePosition = transform.position;
            previouseObjectPosition = rotatingObject.position;
            previousObjectRotation = rotatingObject.rotation;
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            m_handController = null;
            rotatingObject.position = previouseObjectPosition;
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
            Vector3 controllerPosition = m_handController.transform.position;
            Vector3 prevHandleRelativePosition = previousHandlePosition - previouseObjectPosition;
            Vector3 handleRelativePosition = controllerPosition - previouseObjectPosition;
            
            // Calculate the rotation wrt the controller
            Vector3 previousDirection = prevHandleRelativePosition;
            previousDirection.y = 0;
            Vector3 currentDirection = handleRelativePosition;
            currentDirection.y = 0;
            
            Quaternion rotationToApply = Quaternion.FromToRotation(previousDirection, currentDirection);
            rotatingObject.rotation = rotationToApply * previousObjectRotation;
            
            // calculate correct translation
            float translateRatio = Vector3.Dot(currentDirection,  previousDirection.normalized) - 
                                   Vector3.Dot(previousDirection,  previousDirection.normalized);
            powerRatio = translateRatio + 0.5f;
            translateRatio *= 0.7f;

            Vector3 translationToApply = translateRatio * previousDirection.normalized;
            rotatingObject.position = previouseObjectPosition + translationToApply;
            
        }

        public float GetPowerRatio()
        {
            return powerRatio;
        }
    }
    