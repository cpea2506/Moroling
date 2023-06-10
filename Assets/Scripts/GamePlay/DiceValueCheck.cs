using System;
using System.Collections;
using UnityEngine;

public class DiceValueCheck : MonoBehaviour
{
    public static event Action OnTossingDone;

    private GamePlayInfo gamePlayInfo;

    private void Start()
    {
        gamePlayInfo = ServiceManager.service.Get<GamePlayInfo>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // the actual value is the value of the opposite side
            gamePlayInfo.diceValue = gameObject.name switch
            {
                "Side 1" => 6,
                "Side 2" => 5,
                "Side 3" => 4,
                "Side 4" => 3,
                "Side 5" => 2,
                "Side 6" => 1,
                _ => 0 // cannot be reached
            };

            StartCoroutine(StopTossing());
        }
    }

    private IEnumerator StopTossing()
    {
        yield return new WaitForSeconds(1.5f);

        OnTossingDone?.Invoke();
        gamePlayInfo.canToss = false;
    }
}
