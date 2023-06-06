using Cinemachine;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static int currentNumber;

    private Rigidbody diceRigidbody;

    [SerializeField]
    private CinemachineVirtualCamera followCamera;

    private void Start()
    {
        diceRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (
            ServiceManager.service.Get<PlayerInputAction>().Player.TossingDice.IsPressed()
            && IsGrounded
        )
        {
            diceRigidbody.useGravity = true;

            float x = Random.Range(0f, 500f);
            float y = Random.Range(0f, 500f);
            float z = Random.Range(0f, 500f);
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;
            diceRigidbody.AddForce(transform.up * 500);
            diceRigidbody.AddTorque(x, y, z);
        }
    }

    private bool IsGrounded => Mathf.Approximately(diceRigidbody.velocity.sqrMagnitude, 0f);
}
