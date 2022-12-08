using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HonestMistake.CarPiece.UI
{
    public class CarPieceInventoryViewItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        public void AnimateGotItem()
        {
            icon.DOFade(1, 1);
        }

#if UNITY_EDITOR
        public void ResetAnimateGotItem()
        {
            icon.DOFade(0.3f, 0);
        }
#endif
    }
}