using DG.DOTweenEditor;
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
            base.OnInspectorGUI();
            if (GUILayout.Button("Preview animation"))
            {
                var tween = (target as CarPieceKeeper)?.CreateCollectTween();
                DOTweenEditorPreview.PrepareTweenForPreview(tween);
                DOTweenEditorPreview.Start();
            }
        }
    }
}