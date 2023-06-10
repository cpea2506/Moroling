using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Transform GetChild(int index) => transform.GetChild(index);

    public int Length => transform.childCount;
}
