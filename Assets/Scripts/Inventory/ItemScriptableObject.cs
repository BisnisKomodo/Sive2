using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New Item", menuName = "Survival Game/Inventory/New Item")]

public class ItemScriptableObject : ScriptableObject
{
    public enum ItemType {Generic, Consumable, Weapon, MeleeWeapon, Buildable}

    [Header("General")]
    public ItemType itemtype;
    public Sprite icon;
    public string ItemName = "new item";
    public string Description = "new item description";
    public bool IsStackable;
    public int maxstack = 1;


    [Header("Weapon")]
    public float damage = 20f;
    public float range = 200f;
    public float fireRate = 0.1f;
    public int magSize = 30;
    public ItemScriptableObject bulletData;
    [Space]
    public float zoomFOV = 50f;

    [Space]
    public float horizontalRecoil;
    public float minVerticalRecoil;
    public float maxVerticalRecoil;
    [Space]
    [Space]
    public float hipSpread = 0.04f;
    public float aimSpread = 0;
    [Space]
    public bool shotgunFire;
    public int pelletPerShot = 8;
    [Space]
    [Space]
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip drawSound;
    public AudioClip emptySound;


    [Header("Consumable")]
    public float healthChange = 10f;
    public float hungerChange = 10f;
    public float thirstChange = 10f;



    [Header("Buildable")]
    public BuildGhost ghost;
}


