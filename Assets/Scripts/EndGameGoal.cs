using DG.Tweening;
using HonestMistake.CarPiece;
using UnityEngine;

public class EndGameGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryEndGame();    
        }
    }

    private void TryEndGame()
    {
        if (CarPieceInventoryManager.Instance.HasAllItems())
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        float i = 1;
        DOTween.To(() => i, x => i = x, 0, 1).OnComplete(ShowEndScreen);
    }

    private void ShowEndScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        EndScreenManager.Instance.ShowEndScreen();
    }
}