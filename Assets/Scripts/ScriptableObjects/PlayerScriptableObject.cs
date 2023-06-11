using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerScriptableObject",
    menuName = "ScriptableObjects/PlayerScriptableObject"
)]
public class PlayerScriptableObject : ScriptableObject
{
    public List<string> playerNames = new List<string>();
}
