using System;
using TMPro;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private int currentPlayerIndex;
    private GamePlayInfo gamePlayInfo;

    [SerializeField]
    private TextMeshProUGUI turnTextField;

    [SerializeField]
    private Dice dice;

    [SerializeField]
    private Player[] players;

    private void Start()
    {
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
        turnTextField.enabled = true;

        SetPlayerPriority();

        gamePlayInfo.players = players;
    }

    private void SetPlayerPriority()
    {
        foreach (Player player in players)
        {
            player.info.priority = UnityEngine.Random.Range(1, 5);
        }

        Array.Sort(players, (p1, p2) => p1.info.priority.CompareTo(p2.info.priority));
    }

    private void PlayerTurn()
    {
        currentPlayerIndex %= players.Length;

        for (int i = 0; i < players.Length; i++)
        {
            gamePlayInfo.players[i].isCurrentTurn = i == currentPlayerIndex;
        }

        gamePlayInfo.players[currentPlayerIndex].nextPos += gamePlayInfo.diceValue;
        gamePlayInfo.players[currentPlayerIndex].info.turnCount += 1;
    }

    private void NextPlayer()
    {
        currentPlayerIndex += 1;
        while (gamePlayInfo.players[currentPlayerIndex].AtFinalDestination)
        {
            currentPlayerIndex += 1;
        }

        turnTextField.text = gamePlayInfo.players[currentPlayerIndex].info.name;
        gamePlayInfo.canToss = true;
    }

    private void OnEnable()
    {
        DiceValueCheck.OnTossingDone += PlayerTurn;
        Player.OnMovingDone += NextPlayer;
    }

    private void OnDisable()
    {
        DiceValueCheck.OnTossingDone -= PlayerTurn;
        Player.OnMovingDone -= NextPlayer;
    }

    private void CheckGameOver()
    {
        // assume that all pawns have finish their path
        bool allFinish = true;

        foreach (var player in players)
        {
            if (!player.AtFinalDestination)
            {
                allFinish = false;
                break;
            }
        }

        if (allFinish)
        {
            gamePlayInfo.gameState = GameState.Over;
        }
    }

    private void Update()
    {
        dice.gameObject.SetActive(gamePlayInfo.canToss);

        CheckGameOver();
    }
}
