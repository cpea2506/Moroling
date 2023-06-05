using UnityEngine;

public class Dice : MonoBehaviour
{
    private int currentNumber;

    private Rigidbody diceRigidbody;
    private Vector3 diceVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        diceRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        diceVelocity = diceRigidbody.velocity;

        // if (ServiceManager.service.Get<PlayerInputAction>().Player.RollDice.IsPressed())
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Debug.Log("I'm rolling");
            diceRigidbody.useGravity = true;

            float x = Random.Range(0f, 500f);
            float y = Random.Range(0f, 500f);
            float z = Random.Range(0f, 500f);
            transform.position = new Vector3(0, 0.2f, 0);
            transform.rotation = Quaternion.identity;
            diceRigidbody.AddForce(transform.up * 500);
            diceRigidbody.AddTorque(x, y, z);
        }
    }

    private bool IsGrounded => Mathf.Approximately(diceRigidbody.velocity.sqrMagnitude, 0f);
}
