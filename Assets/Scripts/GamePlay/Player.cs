using System;
using TMPro;
using Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

public class Player : MonoBehaviour
{
    private GamePlayInfo gamePlayInfo;
    private int currentPos;

    [SerializeField]
    private Transform[] tiles;

    [SerializeField]
    private TextMeshProUGUI nameTextField;

    [SerializeField]
    private CinemachineVirtualCamera followCamera;

    [NonSerialized]
    public bool isCurrentTurn;

    [NonSerialized]
    public PlayerInfo info;

    [NonSerialized]
    public int nextPos;

    public static event Action OnMovingDone;

    private void Start()
    {
        info = new PlayerInfo();
        info.rank = Rank.None;
        isCurrentTurn = false;
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
        currentPos = 0;
        nextPos = 0;

        // nameTextField.enabled = true;
        // playerInfo.name = nameTextField.text;
    }

    private void Update()
    {
        followCamera.Priority = isCurrentTurn ? 10 : 0;

        Move();
    }

    private void Move()
    {
        if (AtFinalDestination)
        {
            isCurrentTurn = false;
            OnMovingDone?.Invoke();
        }
        else if (isCurrentTurn)
        {
            if (currentPos <= nextPos)
            {
                var currentTile = tiles[currentPos];
                var direction = (currentTile.position - transform.position).normalized;

                if (Vector3.Distance(currentTile.position, transform.position) > 0.01f)
                {
                    transform.position += direction * Time.deltaTime;
                }
                else
                {
                    currentPos++;
                }
            }
            else
            {
                isCurrentTurn = false;
                OnMovingDone?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (AtFinalDestination)
        {
            gamePlayInfo.playerRank += 1;
            info.rank = (Rank)gamePlayInfo.playerRank;
        }
    }

    private bool AtFinalDestination => currentPos == tiles.Length;
    private bool IsOnTrap => false;
}
