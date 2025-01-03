using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;

public class RecipeTemplate : MonoBehaviour, IPointerDownHandler
{
    private CraftingManager crafting;
    [HideInInspector] public CraftingRecipeScriptableObject recipe;
    public Image icon;
    public Text nameText;
    public Text requirementText;
    public Text descriptionText;
    public Text timerText;

    public void Start()
    {
        crafting = GetComponentInParent<CraftingManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            crafting.Try_Craft(this);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            crafting.Cancel(this);
        }
    }
}
