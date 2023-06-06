using UnityEngine;
using TMPro;

public enum Rank
{
    None,
    First,
    Second,
    Third,
    Last,
}

public struct PlayerInfo
{
    public Rank rank;
    public string name;
    public int turnCount;
    public int bonusSectorCount;
    public int failSectorCount;
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameTextField;

    private bool isCurrentTurn;
    private PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = new PlayerInfo();
        playerInfo.rank = Rank.None;

        // nameTextField.enabled = true;
        // playerInfo.name = nameTextField.text;

        isCurrentTurn = false;
    }

    private void Update()
    {
        if (isCurrentTurn)
        {
            playerInfo.turnCount += 1;
        }
    }

    private void OnTriggerEnter(Collider other) { }
}
