using System.Collections.Generic;
using System.Linq;
using HonestMistake.CarPiece.UI;
using HonestMistake.Utils;
using UnityEngine;

namespace HonestMistake.CarPiece
{
    public class CarPieceInventoryManager : Singleton<CarPieceInventoryManager>
    {
        [SerializeField] private CarPieceInventoryView view;
        
        private Dictionary<CarPieceEnum, bool> inventory = new();

        public void GotItem(CarPieceEnum item)
        {
            inventory[item] = true;
            view.GotItem(item);
        }

        public bool HasAllItems()
        {
            return inventory.All(i => i.Value);
        }
    }

    public enum CarPieceEnum
    {
        WHEEL,
        GAS,
        ELECTRIC,
        TIRE,
        WRENCH
    }
}