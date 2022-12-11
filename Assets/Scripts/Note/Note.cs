using System;
using HonestMistake.Interactable;
using TMPro;
using UnityEngine;

namespace HonestMistake.Note
{
    public class Note : InteractableBase
    {
        [Header("Note")] 
        [TextArea] 
        [SerializeField] private string content;
        [SerializeField] private TMP_FontAsset font;

        public override void Interact()
        {
            base.Interact();
            OpenNote();
        }

        private void OpenNote()
        {
            NoteManager.Instance.SetFont(font);
            NoteManager.Instance.DisplayNote(content);
        }
    }
}