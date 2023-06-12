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
    private CinemachineVirtualCamera followCamera;

    [SerializeField]
    private GameObject pawnVisual;

    [SerializeField]
    private Tiles tiles;

    [NonSerialized]
    public bool isCurrentTurn;

    [NonSerialized]
    public PlayerInfo info;

    [NonSerialized]
    public int targetPos;

    [NonSerialized]
    public Vector3 direction;

    public static event Action OnMovingDone;

    private void Start()
    {
        info = new PlayerInfo();
        info.rank = Rank.None;

        currentPos = 0;
        targetPos = 22;
        isCurrentTurn = false;

        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
    }

    private void FixedUpdate()
    {
        if (gamePlayInfo.gameState == GameState.Playing)
        {
            followCamera.Priority = isCurrentTurn ? 10 : 0;

            if (isCurrentTurn)
            {
                if (currentPos <= targetPos && !AtFinalDestination)
                {
                    MoveAndRotate();
                }
                else
                {
                    isCurrentTurn = false;
                    OnMovingDone?.Invoke();
                }
            }
        }
    }

    private void MoveAndRotate()
    {
        var nextTile = tiles.GetChild(currentPos);

        direction = (nextTile.position - transform.position).normalized;

        if (Vector3.Distance(nextTile.position, transform.position) >= 0.01f)
        {
            transform.position += direction * Time.deltaTime;
            pawnVisual.transform.LookAt(nextTile.position);
        }
        else
        {
            currentPos += 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("End"))
        {
            currentPos = tiles.Length;
            gamePlayInfo.playerRank += 1;
            info.rank = (Rank)gamePlayInfo.playerRank;
        }
    }

    public bool AtFinalDestination => currentPos == tiles.Length;
}
