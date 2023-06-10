using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCanvas;

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
            gameOverCanvas.SetActive(true);
        }
    }

    private void SetScoreBoard()
    {
        foreach (var player in gamePlayInfo.players)
        {
            if (player.info.rank != Rank.None && player.info.rank == (Rank)gamePlayInfo.playerRank)
            {
                playerNames[gamePlayInfo.playerRank - 1].text = player.info.name;
                playerTurns[gamePlayInfo.playerRank - 1].text = player.info.turnCount.ToString();
            }
        }
    }
}
