using System;
using HonestMistake.Utils;
using StarterAssets;
using TMPro;
using UnityEngine;

namespace HonestMistake.Note
{
    public class NoteManager : Singleton<NoteManager>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI contentText;
        
        private StarterAssetsInputs _inputs;
        private bool isNoteOpen;

        private void Start()
        {
            _inputs = FindObjectOfType<StarterAssetsInputs>();
        }

        public void DisplayNote(string content)
        {
            contentText.text = content;
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            _inputs.SetInteractListener(OnInteracted);
        }

        private void OnInteracted(bool didPress)
        {
            if (didPress)
            {
                CloseNote();
            }
        }

        private void CloseNote()
        {
            canvas.gameObject.SetActive(false);
            _inputs.StopListeningToInteract(OnInteracted);
            Time.timeScale = 1;
        }
    }
}