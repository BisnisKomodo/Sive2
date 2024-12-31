using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private InventoryManager inventory;
    public RecipeTemplate recipeTemplate;
    public CraftingRecipeScriptableObject[] recipes;
    public Transform contentHolder;


    //private variables
    public RecipeTemplate recipeInCraft;
    private bool isCrafting;
    private float currentTimer;

    public bool opened;

    public void Start()
    {
        inventory = GetComponentInParent<InventoryManager>();
        GenerateRecipes();
    }

    private void Update()
    {
        if (isCrafting)
        {
            if (currentTimer > 0)
            {
                recipeInCraft.timerText.text = currentTimer.ToString("f1");
            }

            else
            {
                recipeInCraft.timerText.text = "";

                inventory.AddItem(recipeInCraft.recipe.outcome, recipeInCraft.recipe.outcomeAmount);

                isCrafting = false;
            }

            currentTimer -= Time.deltaTime;
        }

        if (opened)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            transform.position = new Vector3(-10000, 0, 0);
        }
    }

    public void GenerateRecipes()
    {
        for (int i = 0;  i < recipes.Length; i++)
        {
            RecipeTemplate recipe = Instantiate(recipeTemplate.gameObject, contentHolder).GetComponent<RecipeTemplate>();


            recipe.recipe = recipes[i];
            recipe.icon.sprite = recipes[i].icon;
            recipe.nameText.text = recipes[i].recipeName;
            recipe.timerText.text = "";

            for (int b = 0; b < recipes[i].requirements.Length; b++)
            {
                if (b == 0)
                {
                    recipe.requirementText.text = $"{recipes[i].requirements[b].data.ItemName} {recipes[i].requirements[b].amountNeeded}";
                }
                else
                {
                    recipe.requirementText.text = $"{recipe.requirementText.text}, {recipes[i].requirements[b].data.ItemName} {recipes[i].requirements[b].amountNeeded}\n";
                }
            }
        }
    }

    public void Try_Craft(RecipeTemplate template)
    {
        if (!HasResources(template.recipe) || isCrafting)
        {
            return;
        }

        TakeResources(template.recipe);

        recipeInCraft = template;
        isCrafting = true;
        currentTimer = template.recipe.craftingTime;
    }       

    public void Cancel(RecipeTemplate template)
    {
        if (!isCrafting)
        {
            return;
        }

        for (int i = 0; i < template.recipe.requirements.Length; i++)
        {
            inventory.AddItem(template.recipe.requirements[i].data, template.recipe.requirements[i].amountNeeded);
        }

        isCrafting = false;
        recipeInCraft.timerText.text = "";
    }

    public bool HasResources(CraftingRecipeScriptableObject recipe)
    {
        bool canCraft = true;

        int[] stacksNeeded = null;
        int[] stacksAvailable = null;
        List<int> stacksNeededList = new List<int>();

        //Mencari stacks yang diperlukan
        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            stacksNeededList.Add(recipe.requirements[i].amountNeeded);
        }
        stacksNeeded = stacksNeededList.ToArray();
        stacksAvailable = new int[stacksNeeded.Length];

        //Check untuk itemnya

        for (int b = 0; b < recipe.requirements.Length; b++)
        {
            for (int i = 0; i < inventory.inventorySlots.Length; i++)
            {
                if(inventory.inventorySlots[i].data == recipe.requirements[b].data)
                {
                    stacksAvailable[b] += inventory.inventorySlots[i].StackSize;
                }
            }
        }

        //Check kalau bisa di craft
        for (int i = 0; i < stacksAvailable.Length; i++)
        {
            if (stacksAvailable[i] < stacksNeeded[i])
            {
                canCraft = false;
                break;
            }
        }
        return canCraft;
    }

    public void TakeResources(CraftingRecipeScriptableObject recipe)
    {
        int[] stacksNeeded = null;
        
        List<int> stacksNeededList = new List<int>();

        //Mencari stacks yang diperlukan
        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            stacksNeededList.Add(0);
        }
        stacksNeeded = stacksNeededList.ToArray();

        //Mengambil items
        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            for (int b = 0; b < inventory.inventorySlots.Length; b++)
            {
                if (!inventory.inventorySlots[b].IsEmpty)
                {
                    
                if (inventory.inventorySlots[b].data == recipe.requirements[i].data)
                {
                    if (stacksNeeded[i] < recipe.requirements[i].amountNeeded)
                    {
                        if (stacksNeeded[i] + inventory.inventorySlots[b].StackSize > recipe.requirements[i].amountNeeded)
                        {
                            int amountLeftOnSlot = (inventory.inventorySlots[b].StackSize + stacksNeeded[i]) - recipe.requirements[i].amountNeeded;
                            inventory.inventorySlots[b].StackSize = amountLeftOnSlot;
                            stacksNeeded[i] = recipe.requirements[i].amountNeeded;
                        }
                        else
                        {
                            stacksNeeded[i] += inventory.inventorySlots[b].StackSize;

                            inventory.inventorySlots[b].Clean();
                        }
                    }


                    inventory.inventorySlots[b].UpdateSlot();
                }
                }
            }
        }   
    }
}
