using UnityEngine;

namespace HonestMistake.Interactable
{
    public class InteractableBase : Interactable
    {
        [Header("Interactable Base")]
        [SerializeField] private GameObject interactHint;
        [SerializeField] private bool isRepeatable;
        
        private bool wasEverInteracted;
        
        public void SpottedByChecker()
        {
            if (!wasEverInteracted || isRepeatable)
            {
                interactHint.SetActive(true);
            }
        }

        public void AbandonedByChecker()
        {
            HideHint();
        }

        private void HideHint()
        {
            interactHint.SetActive(false);
        }

        public override void Interact()
        {
            wasEverInteracted = true;
            interactHint.SetActive(false);
        }
    }
}