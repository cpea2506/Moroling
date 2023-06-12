using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreboardMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreBoardCanvas;

    [SerializeField]
    private TextMeshProUGUI[] playerNames;

    [SerializeField]
    private TextMeshProUGUI[] playerTurns;

    [SerializeField]
    private GamePlayManager gamePlayManager;

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
            SoundManager.instance.PlaySound(SFX.AtTheEnd, 0.1f);
        }
    }

    private void SetScoreBoard()
    {
        foreach (var player in gamePlayManager.players)
        {
            if (player.info.rank != Rank.None && player.info.rank == (Rank)gamePlayInfo.playerRank)
            {
                playerNames[gamePlayInfo.playerRank - 1].text = player.info.name;
                playerTurns[gamePlayInfo.playerRank - 1].text = player.info.turnCount.ToString();
            }
        }
    }
}
