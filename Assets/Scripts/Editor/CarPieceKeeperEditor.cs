using DG.DOTweenEditor;
using HonestMistake.CarPiece;
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
            GUILayout.Label("Editor Testing");
            if (GUILayout.Button("Preview animation"))
            {
                var tween = (target as CarPieceKeeper)?.CreateCollectTween();
                DOTweenEditorPreview.PrepareTweenForPreview(tween);
                DOTweenEditorPreview.Start();
            }
            
            GUILayout.Space(20);

            GUILayout.Label("Runtime Testing");
            if (GUILayout.Button("Test Collect"))
            {
                if(!Application.isPlaying) Debug.LogError("You need to be in playmode to test this!");
                
                (target as CarPieceKeeper)?.Interact();
            }
            
            if (GUILayout.Button("Reset Collect"))
            {
                if(!Application.isPlaying) Debug.LogError("You need to be in playmode to test this!");
                
                (target as CarPieceKeeper)?.ResetCollect();
            }
        }
    }
}