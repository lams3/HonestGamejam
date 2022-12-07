using DG.Tweening;
using UnityEngine;

namespace HonestMistake.Interactable.CarPieces
{
    public class CarPieceKeeper : MonoBehaviour, IInteractableHandler
    {
        [SerializeField] private Transform pieceToCollect;

        [Header("Animation")] 
        [SerializeField] private float animDuration = 1;
        [SerializeField] private float finalHeight;
        [SerializeField] private Vector3 punch;
        
#if UNITY_EDITOR
        private Vector3 originalPos;
        private Vector3 originalScale;
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
            Vector3 finalMovePos = pieceToCollect.position + Vector3.up * finalHeight;
            
            var seq = DOTween.Sequence();
            seq.Insert(0, pieceToCollect.DOMove(finalMovePos, animDuration));
            seq.AppendInterval(animDuration / 3);
            seq.Insert(0, pieceToCollect.DOPunchRotation(punch, animDuration));
            seq.Insert(0, pieceToCollect.DOScale(pieceToCollect.localScale * 1.25f, animDuration / 2));
            seq.Insert(animDuration / 2, pieceToCollect.DOScale(0, animDuration / 2));

#if UNITY_EDITOR
            originalPos = pieceToCollect.position;
            originalRot = pieceToCollect.rotation;
            originalScale = pieceToCollect.localScale;

            seq.Insert(animDuration, pieceToCollect.DOMove(originalPos, 0));
            seq.Insert(animDuration, pieceToCollect.DOScale(originalScale, 0));
            seq.Insert(animDuration, pieceToCollect.DORotateQuaternion(originalRot, 0));
#endif

            return seq;
        }
    }
}
