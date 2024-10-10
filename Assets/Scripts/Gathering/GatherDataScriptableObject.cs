using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Gather Data", menuName = "Survival Game/Gathering/Gather Data")]

public class GatherDataScriptableObject : ScriptableObject
{
    public ItemScriptableObject item;
    public int amount;
}
