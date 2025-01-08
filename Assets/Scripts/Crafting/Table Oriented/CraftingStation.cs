// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CraftingStation : MonoBehaviour
// {
//     public GameObject craftingUI;
//     public bool opened = false;

//     private void Start()
//     {
//         FindCauldronCraftingUI();
//     }

//     private void FindCauldronCraftingUI()
//     {
//         craftingUI = FindInactiveObjectByName(null, "CauldronCrafting"); 
//         // if (craftingUI == null)
//         // {
//         //     Debug.LogError("CauldronCrafting UI cannot be found");
//         // }
//         // else
//         // {
//         //     Debug.Log("CauldronCrafting UI found: " + craftingUI.name);
//         // }
//     }

//     private GameObject FindInactiveObjectByName(Transform parent, string name)
//     {
//         if (parent == null)
//         {
//             foreach (GameObject rootObj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
//             {
//                 GameObject result = FindInactiveObjectByName(rootObj.transform, name);
//                 if (result != null)
//                 {
//                     return result;
//                 }
//             }
//         }
//         else
//         {
//             foreach (Transform child in parent)
//             {
//                 if (child.name == name)
//                 {
//                     return child.gameObject;
//                 }

//                 GameObject result = FindInactiveObjectByName(child, name);
//                 if (result != null)
//                 {
//                     return result;
//                 }
//             }
//         }

//         return null;
//     }

//     public void Open()
//     {
//         if (!opened)
//         {
//             if (craftingUI != null)
//             {
//                 craftingUI.SetActive(true); 
//             }

//             opened = true; 
//         }
//     }
//     public void Close()
//     {
//         if (opened)
//         {
//             if (craftingUI != null)
//             {
//                 craftingUI.SetActive(false); 
//             }

//             opened = false; 
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public bool opened = false;
    public void Open()
    {
        if (!opened)
        {
            UIManager.instance.SetCauldronCraftingUI(true);
            FindObjectOfType<WindowHandler>().SetCauldronUIState(true);
            opened = true; 
        }
    }
    public void Close()
    {
        if (opened)
        {
            UIManager.instance.SetCauldronCraftingUI(false);
            FindObjectOfType<WindowHandler>().SetCauldronUIState(false);
            opened = false; 
        }
    }
}
