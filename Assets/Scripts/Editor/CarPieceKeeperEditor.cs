using HonestMistake.Interactable.CarPieces;
using UnityEditor;
using UnityEngine;

namespace HonestMistake.Editor
{
    [CustomEditor(typeof(CarPieceKeeper))]
    public class CarPieceKeeperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            if (GUILayout.Button("Test collect"))
            {
                (target as CarPieceKeeper)?.OnInteracted();
            }
            
            if (GUILayout.Button("Reset collect"))
            {
                (target as CarPieceKeeper)?.DebugResetCollect();
            }
        }
    }
}