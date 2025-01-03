using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Survival Game/Crafting/New Recipe")]
public class CraftingRecipeScriptableObject : ScriptableObject
{
    public Sprite icon;
    public string recipeName;
    public string recipeDescription;
    public CraftingRequirement[] requirements;
    [Space]
    public float craftingTime;
    [Space]
    public ItemScriptableObject outcome;
    public int outcomeAmount = 1;
}
