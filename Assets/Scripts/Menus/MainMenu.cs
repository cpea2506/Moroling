using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerScriptableObject playerManagerValue;

    public void LoadInGame()
    {
        playerManagerValue.playerNames = new List<string>();
        SceneManager.LoadScene("GamePlay");
    }
}
