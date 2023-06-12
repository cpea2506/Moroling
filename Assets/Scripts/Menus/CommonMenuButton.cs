using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CommonMenuButton : MonoBehaviour
{
    [SerializeField]
    private PlayerScriptableObject playerManagerValue;

    public void ToMainMenu()
    {
        playerManagerValue.playerNames = new List<string>();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
