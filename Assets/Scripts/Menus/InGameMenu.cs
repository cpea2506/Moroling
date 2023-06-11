using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerScriptableObject playerManagerValue;

    [SerializeField]
    private Player[] players;

    [SerializeField]
    private TMP_InputField[] inputFields;

    [SerializeField]
    private GameObject nameInputCanvas;

    private GamePlayInfo gamePlayInfo;

    private void Start()
    {
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();

        if (playerManagerValue.playerNames.Count != 0)
        {
            for (int i = 0; i < playerManagerValue.playerNames.Count; i++)
            {
                players[i].info.name = playerManagerValue.playerNames[i];
            }

            nameInputCanvas.SetActive(false);
        }
    }

    public void StartGame()
    {
        int i = 0;
        foreach (var inputField in inputFields)
        {
            players[i].info.name = inputField.text;
            playerManagerValue.playerNames.Add(inputField.text);

            i += 1;
        }

        nameInputCanvas.SetActive(false);
        gamePlayInfo.gameState = GameState.Playing;
    }
}
