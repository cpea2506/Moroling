using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    public static Service service { get; } = new Service();

    private void Awake()
    {
        service.Register(new PlayerInputAction());
        service.Register(new GamePlayInfo());
    }

    private void OnEnable()
    {
        service.Get<PlayerInputAction>().Enable();
    }
}
