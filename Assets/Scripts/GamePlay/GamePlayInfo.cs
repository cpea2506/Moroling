public struct PlayerInfo
{
    public Rank rank;
    public string name;
    public int turnCount;
    public int bonusSectorCount;
    public int failSectorCount;
    public int priority;
}

public enum GameState
{
    Over,
    Playing,
    Idle,
}

public enum Rank
{
    None,
    First,
    Second,
    Third,
    Last,
}

public class GamePlayInfo
{
    public int playerRank = (int)Rank.None;
    public int diceValue = 0;
    public bool canToss = true;
    public GameState gameState = GameState.Idle;
}
