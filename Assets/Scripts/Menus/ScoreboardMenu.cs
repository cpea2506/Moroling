using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreboardMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerScriptableObject playerManagerValue;

    [SerializeField]
    private GameObject scoreBoardCanvas;

    [SerializeField]
    private TextMeshProUGUI[] playerNames;

    [SerializeField]
    private TextMeshProUGUI[] playerTurns;

    private GamePlayInfo gamePlayInfo;

    private void Start()
    {
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
    }

    private void Update()
    {
        SetScoreBoard();

        if (gamePlayInfo.gameState == GameState.Over)
        {
            scoreBoardCanvas.SetActive(true);
        }
    }

    private void SetScoreBoard()
    {
        foreach (var player in gamePlayInfo.players)
        {
            if (player.info.rank != Rank.None && player.info.rank == (Rank)gamePlayInfo.playerRank)
            {
                Debug.Log(
                    $"{player.info.name}: {player.info.turnCount}, {gamePlayInfo.playerRank}"
                );
                playerNames[gamePlayInfo.playerRank - 1].text = player.info.name;
                playerTurns[gamePlayInfo.playerRank - 1].text = player.info.turnCount.ToString();
            }
        }
    }

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
