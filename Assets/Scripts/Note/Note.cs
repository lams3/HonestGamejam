using System;
using HonestMistake.Interactable;
using UnityEngine;

namespace HonestMistake.Note
{
    public class Note : InteractableBase
    {
        [Header("Note")] 
        [TextArea] 
        [SerializeField] private string content;

        public override void Interact()
        {
            base.Interact();
            OpenNote();
        }

        private void OpenNote()
        {
            NoteManager.Instance.DisplayNote(content);
        }
    }
}