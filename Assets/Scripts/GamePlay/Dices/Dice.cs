using Cinemachine;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera followCamera;

    private Rigidbody diceRigidbody;
    private GamePlayInfo gamePlayInfo;
    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        diceRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerInputAction = ServiceManager.service.Get<PlayerInputAction>();
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
    }

    private void OnEnable()
    {
        diceRigidbody.useGravity = false;
        diceRigidbody.isKinematic = true;
        transform.position = Vector3.zero;
        transform.eulerAngles = new Vector3(-48, 228, 131);
        followCamera.transform.position = new Vector3(-0.3f, 3f, -0.5f);
    }

    private void FixedUpdate()
    {
        if (gamePlayInfo.gameState == GameState.Playing)
        {
            if (
                gamePlayInfo.canToss
                && playerInputAction.Player.TossingDice.IsPressed()
                && IsGrounded
            )
            {
                diceRigidbody.isKinematic = false;
                diceRigidbody.useGravity = true;

                float x = UnityEngine.Random.Range(0f, 500f);
                float y = UnityEngine.Random.Range(0f, 500f);
                float z = UnityEngine.Random.Range(0f, 500f);

                transform.position = new Vector3(0, 2f, 0);
                transform.rotation = Quaternion.identity;
                diceRigidbody.AddForce(transform.up * 50f);
                diceRigidbody.AddTorque(x, y, z);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.PlaySound(SFX.BouncingDice);
        }
    }

    private bool IsGrounded => Mathf.Approximately(diceRigidbody.velocity.sqrMagnitude, 0f);
}
