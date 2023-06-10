using TMPro;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField[] inputFields;

    [SerializeField]
    private GameObject nameInputCanvas;

    private GamePlayInfo gamePlayInfo;

    private void Start()
    {
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
    }

    public void StartGame()
    {
        int i = 0;

        foreach (var inputField in inputFields)
        {
            gamePlayInfo.players[i].info.name = inputField.text;
            i += 1;
        }

        nameInputCanvas.SetActive(false);
        gamePlayInfo.gameState = GameState.Playing;
    }
}
