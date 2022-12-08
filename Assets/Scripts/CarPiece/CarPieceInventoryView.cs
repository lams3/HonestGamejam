using UnityEngine;

namespace HonestMistake.CarPiece.UI
{
    public class CarPieceInventoryView : MonoBehaviour
    {
        [SerializeField] private CarPieceInventoryViewItem itemWheel;
        [SerializeField] private CarPieceInventoryViewItem itemGas;
        [SerializeField] private CarPieceInventoryViewItem itemElectric;
        [SerializeField] private CarPieceInventoryViewItem itemTire;
        [SerializeField] private CarPieceInventoryViewItem itemWrench;

        public void GotItem(CarPieceEnum item)
        {
            switch (item)
            {
                case CarPieceEnum.WHEEL:
                    itemWheel.AnimateGotItem();
                    break;
                case CarPieceEnum.GAS:
                    itemGas.AnimateGotItem();
                    break;
                case CarPieceEnum.ELECTRIC:
                    itemElectric.AnimateGotItem();
                    break;
                case CarPieceEnum.TIRE:
                    itemTire.AnimateGotItem();
                    break;
                case CarPieceEnum.WRENCH:
                    itemWrench.AnimateGotItem();
                    break;
            }
        }
    }
}