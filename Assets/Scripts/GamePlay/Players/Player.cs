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
    public int nextPos;

    [NonSerialized]
    public Vector3 direction;

    [HideInInspector]
    public bool isFinish;

    public static event Action OnMovingDone;

    private void Start()
    {
        info = new PlayerInfo();
        info.rank = Rank.None;

        isCurrentTurn = false;
        isFinish = false;
        currentPos = 0;
        nextPos = 0;

        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
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
            isCurrentTurn = false;

            return;
        }

        if (isCurrentTurn)
        {
            if (currentPos <= nextPos && !AtFinalDestination)
            {
                var currentTile = tiles.GetChild(currentPos);

                direction = (currentTile.position - transform.position).normalized;

                if (Vector3.Distance(currentTile.position, transform.position) > 0.01f)
                {
                    transform.position += direction * Time.deltaTime;
                    pawnVisual.transform.LookAt(currentTile.position);
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
        if (other.gameObject.name.Contains("End"))
        {
            currentPos = tiles.Length;
            gamePlayInfo.playerRank += 1;
            info.rank = (Rank)gamePlayInfo.playerRank;
        }
    }

    public bool AtFinalDestination => currentPos == tiles.Length;
}
