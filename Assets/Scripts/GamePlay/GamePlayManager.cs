using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private int currentPlayerIndex;
    private GamePlayInfo gamePlayInfo;

    [SerializeField]
    private Dice dice;

    [SerializeField]
    private Player[] players;

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

        players[currentPlayerIndex].nextPos += gamePlayInfo.diceValue;
    }

    private void NextPlayer()
    {
        currentPlayerIndex += 1;
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

    private void Update()
    {
        dice.gameObject.SetActive(gamePlayInfo.canToss);
    }
}
