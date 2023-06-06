using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        string colliderName = other.gameObject.name;

        if (colliderName.Contains("Side"))
        {
            // the actual value is the value of the opposite side
            Dice.currentNumber = colliderName switch
            {
                "Side 1" => 6,
                "Side 2" => 5,
                "Side 3" => 4,
                "Side 4" => 3,
                "Side 5" => 2,
                "Side 6" => 1,
                _ => 0 // cannot be reached
            };
        }
    }
}
