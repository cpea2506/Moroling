using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
