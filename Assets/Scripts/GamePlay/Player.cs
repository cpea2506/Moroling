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
    private Tiles tiles;

    [SerializeField]
    private CinemachineVirtualCamera followCamera;

    [NonSerialized]
    public bool isCurrentTurn;

    [NonSerialized]
    public PlayerInfo info;

    [NonSerialized]
    public int nextPos;

    [HideInInspector]
    public bool isFinish;

    public static event Action OnMovingDone;

    private void Start()
    {
        info = new PlayerInfo();
        info.rank = Rank.None;
        isCurrentTurn = false;
        isFinish = false;
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
        currentPos = 0;
        nextPos = 23;
    }

    private void Update()
    {
        followCamera.Priority = isCurrentTurn ? 10 : 0;

        Move();
    }

    private void Move()
    {
        if (gamePlayInfo.gameState == GameState.Over)
        {
            return;
        }

        if (isCurrentTurn)
        {
            if (currentPos <= nextPos && !AtFinalDestination)
            {
                var currentTile = tiles.GetChild(currentPos);
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
                info.turnCount += 1;
                OnMovingDone?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("end"))
        {
            currentPos = tiles.Length;
            gamePlayInfo.playerRank += 1;
            info.rank = (Rank)gamePlayInfo.playerRank;
        }
    }

    public bool AtFinalDestination => currentPos == tiles.Length;
}
