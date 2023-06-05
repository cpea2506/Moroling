public class GameState
{
    public enum State
    {
        GameOver,
        Playing,
        Idle,
    }

    private State currentState = State.Idle;
}
