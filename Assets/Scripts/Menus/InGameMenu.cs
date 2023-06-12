using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerScriptableObject playerManagerValue;

    [SerializeField]
    private GamePlayManager gamePlayManager;

    [SerializeField]
    private TMP_InputField[] inputFields;

    [SerializeField]
    private GameObject buttonCanvas;

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
                gamePlayManager.players[i].info.name = playerManagerValue.playerNames[i];
            }

            ToInGame();
        }
    }

    private void Update()
    {
        buttonCanvas.SetActive(gamePlayInfo.gameState == GameState.Playing);
    }

    public void StartGame()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            string playerName = inputFields[i].text;

            gamePlayManager.players[i].info.name = playerName;
            playerManagerValue.playerNames.Add(playerName);
        }

        ToInGame();
    }

    private void ToInGame()
    {
        nameInputCanvas.SetActive(false);
        gamePlayInfo.gameState = GameState.Playing;
    }
}
