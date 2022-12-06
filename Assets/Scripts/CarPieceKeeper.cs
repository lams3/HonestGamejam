using UnityEngine;

namespace HonestMistake.Interactable.CarPieces
{
    public class CarPieceKeeper : MonoBehaviour, IInteractableHandler
    {
        [SerializeField] private Transform pieceToCollect;

        public void OnInteracted()
        {
            Collect();
        }

#if UNITY_EDITOR
        public void DebugResetCollect()
        {
            //Remove collected item from inventory service
        }
#endif
        
        private void Collect()
        {
            AddPieceToInventory();
            PlayCollectAnimation();
        }

        private void AddPieceToInventory()
        {
            //add collected item to inventory service
        }

        private void PlayCollectAnimation()
        {
            
        }
    }
}
