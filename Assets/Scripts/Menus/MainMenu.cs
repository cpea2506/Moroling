using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadInGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
