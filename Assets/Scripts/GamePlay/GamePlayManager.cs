using TMPro;
using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private GamePlayInfo gamePlayInfo;
    private int currentPlayerIndex;

    [SerializeField]
    private Dice dice;

    public Player[] players;

    private void Start()
    {
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();

        SetPlayerPriority();
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
            players[i].isCurrentTurn = i == currentPlayerIndex;
        }

        CurrentPlayer.targetPos += gamePlayInfo.diceValue;
        CurrentPlayer.info.turnCount += 1;
    }

    private Player CurrentPlayer => players[currentPlayerIndex % players.Length];

    private void NextPlayer()
    {
        currentPlayerIndex += 1;

        while (CurrentPlayer.AtFinalDestination)
        {
            currentPlayerIndex += 1;
        }

        gamePlayInfo.canToss = true;
    }

    private void OnEnable()
    {
        Player.OnMovingDone += OnMovingDone;
        DiceValueCheck.OnTossingDone += OnTossingDone;
    }

    private void OnDisable()
    {
        Player.OnMovingDone += OnMovingDone;
        DiceValueCheck.OnTossingDone += OnTossingDone;
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
        if (gamePlayInfo.gameState == GameState.Playing)
        {
            CheckGameOver();
        }
    }

    private void OnMovingDone()
    {
        NextPlayer();
        dice.gameObject.SetActive(true);
    }

    private void OnTossingDone()
    {
        PlayerTurn();
        dice.gameObject.SetActive(false);
    }
}
