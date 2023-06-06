using UnityEngine;

public enum GameState
{
    GameOver,
    Playing,
    Idle,
}

public class GamePlayManager : MonoBehaviour
{
    private GameState currentState = GameState.Idle;
    private int turnCount = 0;
    private int currentPlayer = 0;

    [SerializeField]
    private Player[] players;
}
