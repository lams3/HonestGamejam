using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HonestMistake.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : Singleton<EndScreenManager>
{
    [SerializeField] private CanvasGroup endScreen;
    
    public void ShowEndScreen()
    {
        endScreen.DOFade(1, 0.6f);
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }

    public void OnPlayAgainButtonPress()
    {
        //TODO
        //SceneManager.LoadScene()
    }
}
