using DG.Tweening;
using UnityEngine;

namespace HonestMistake.Interactable.CarPieces
{
    public class CarPieceKeeper : MonoBehaviour, IInteractableHandler
    {
        [SerializeField] private Transform pieceToCollect;
        
#if UNITY_EDITOR
        private Vector3 originalPos;
        private Quaternion originalRot;
#endif
        
        public void OnInteracted()
        {
            Collect();
        }
        
        private void Collect()
        {
            AddPieceToInventory();
            PlayCollectAnimation();
        }

        private void AddPieceToInventory()
        {
            //TODO: add collected item to inventory service
        }

        private void PlayCollectAnimation()
        {
            var seq = CreateCollectTween();
            seq.Play().OnComplete(() => pieceToCollect.gameObject.SetActive(false));
        }

        public Sequence CreateCollectTween()
        {
            Vector3 finalMovePos = pieceToCollect.position + Vector3.up * 1;
            float animDuration = 1;
            
            var seq = DOTween.Sequence();
            seq.Insert(0, pieceToCollect.DOMove(finalMovePos, animDuration));
            seq.AppendInterval(animDuration / 3);
            seq.Insert(0, pieceToCollect.DOPunchRotation(Vector3.one * 30, animDuration));

#if UNITY_EDITOR
            originalPos = pieceToCollect.position;
            originalRot = pieceToCollect.rotation;

            seq.Insert(animDuration, pieceToCollect.DOMove(originalPos, 0));
            seq.Insert(animDuration, pieceToCollect.DORotateQuaternion(originalRot, 0));
#endif

            return seq;
        }
    }
}
