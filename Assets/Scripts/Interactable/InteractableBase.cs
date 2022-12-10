using UnityEngine;

namespace HonestMistake.Interactable
{
    public class InteractableBase : Interactable
    {
        [SerializeField] private GameObject interactHint;

        private bool wasEverInteracted = false;
        
        public void SpottedByChecker()
        {
            if (!wasEverInteracted)
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

        public override void OnInteracted()
        {
            wasEverInteracted = true;
            interactHint.SetActive(false);
        }
    }
}