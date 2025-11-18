using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnQuitButton()
    {
        GameManager.Instance.QuitGame();
    }
}