using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    public static Service service { get; } = new Service();

    // Start is called before the first frame update
    private void Awake()
    {
        service.Register(new PlayerInputAction());
        service.Register(new GameState());
        service.Register(new Dice());
    }
}
