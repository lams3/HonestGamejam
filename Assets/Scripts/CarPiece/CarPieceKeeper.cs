using DG.Tweening;
using HonestMistake.Interactable;
using UnityEngine;

namespace HonestMistake.CarPiece
{
    public class CarPieceKeeper : InteractableBase 
    {
        [SerializeField] private Transform pieceToCollect;
        [SerializeField] private CarPieceEnum carPieceEnum;
        
        [Header("Animation")] 
        [SerializeField] private float animDuration = 1;
        [SerializeField] private float finalHeight;
        
#if UNITY_EDITOR
        private Vector3 originalPos;
        private Vector3 originalScale;
        private Quaternion originalRot;
        
        private void Awake()
        {
            CacheTransform();
        }
        
        private void CacheTransform()
        {
            originalPos = pieceToCollect.position;
            originalRot = pieceToCollect.rotation;
            originalScale = pieceToCollect.localScale;
        }
#endif
        
        public override void Interact()
        {
            base.Interact();
            Collect();
        }
        
        private void Collect()
        {
            AddPieceToInventory();
            PlayCollectAnimation();
        }

        private void AddPieceToInventory()
        {
            CarPieceInventoryManager.Instance.GotItem(carPieceEnum);
        }

        private void PlayCollectAnimation()
        {
            var seq = CreateCollectTween();
            seq.Play().OnComplete(() => pieceToCollect.gameObject.SetActive(false));
        }

        public Sequence CreateCollectTween(bool isPreview = false)
        {
            Vector3 finalMovePos = pieceToCollect.position + Vector3.up * finalHeight;
            
            var seq = DOTween.Sequence();
            seq.Insert(0, pieceToCollect.DOMove(finalMovePos, animDuration));
            seq.AppendInterval(animDuration / 3);
            seq.Insert(0, pieceToCollect.DOBlendablePunchRotation(Vector3.one, animDuration));
            seq.Insert(0, pieceToCollect.DOScale(pieceToCollect.localScale * 1.25f, animDuration / 2));
            seq.Insert(animDuration / 2, pieceToCollect.DOScale(0, animDuration / 2));

#if UNITY_EDITOR
            if (isPreview)
            {
                CacheTransform();
                seq.Insert(animDuration, pieceToCollect.DOMove(originalPos, 0));
                seq.Insert(animDuration, pieceToCollect.DOScale(originalScale, 0));
                seq.Insert(animDuration, pieceToCollect.DORotateQuaternion(originalRot, 0));  
            }
#endif

            return seq;
        }

#if UNITY_EDITOR
        public void ResetCollect()
        {
            pieceToCollect.gameObject.SetActive(true);
            pieceToCollect.position = originalPos;
            pieceToCollect.localScale = originalScale;
            pieceToCollect.rotation = originalRot;
            
            CarPieceInventoryManager.Instance.ResetGotItem(carPieceEnum);
        }
#endif
    }
}
