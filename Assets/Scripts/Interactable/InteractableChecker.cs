using System;
using HonestMistake.Note;
using StarterAssets;
using UnityEngine;

namespace HonestMistake.Interactable
{
    public class InteractableChecker : MonoBehaviour
    {
        [SerializeField] private Transform raycastOrigin;
        [SerializeField] private float maxDist;
        [SerializeField] private LayerMask checkMask;

        [SerializeField] private StarterAssetsInputs _input;

        private InteractableBase gazingInteractable;
        private bool isInCooldown;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + raycastOrigin.forward * maxDist);
        }

        private void Start()
        {
            NoteManager.Instance.OnNoteClosed -= TriggerCooldown;
            NoteManager.Instance.OnNoteClosed += TriggerCooldown;
        }

        private void Update()
        {
            if (isInCooldown)
            {
                return;
            }
            
            RaycastHit hit;
            if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, maxDist, checkMask))
            {
                var interactable = hit.collider.GetComponent<InteractableBase>();
                if (interactable)
                {
                    if (interactable != gazingInteractable)
                    {
                        FoundInteractable(interactable);
                    }
                }
                else
                {
                    ClearFoundInteractable();
                }
            }
            else
            {
                ClearFoundInteractable();
            }

            CheckInteractInput();
        }

        private void CheckInteractInput()
        {
            if (_input.interact)
            {
                if (gazingInteractable != null)
                {
                    gazingInteractable.Interact();   
                }
            }
        }

        private void ClearFoundInteractable()
        {
            if (!gazingInteractable) 
                return;
                    
            gazingInteractable.AbandonedByChecker();
            gazingInteractable = null;
        }

        private void FoundInteractable(InteractableBase foundInteractable)
        {
            gazingInteractable = foundInteractable;
            foundInteractable.SpottedByChecker();
        }
        
        public void TriggerCooldown()
        {
            isInCooldown = true;
            Invoke(nameof(ClearCooldown), 0.2f);
        }

        private void ClearCooldown()
        {
            isInCooldown = false;
        }

        private void OnDestroy()
        {
            NoteManager.Instance.OnNoteClosed -= TriggerCooldown;
        }
    }
}