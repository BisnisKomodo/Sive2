using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerMovement player;
    private AudioSource audioS;


    [HideInInspector] public Animator anim;
    [HideInInspector] public Slot slotEquippedOn;
    public GameObject bulletHolePrefab;
    public ItemScriptableObject weaponData;
    public bool isAutomatic;
    public ParticleSystem muzzleFlash;
    [Space]
    public Transform shootPoint;
    public LayerMask shootableLayers;
    [Header("Aiming")]
    public float aimSpeed = 10;
    public Vector3 hipPos;
    public Vector3 aimPos;
    public bool isAiming;

    [HideInInspector] public bool isReloading;
    [HideInInspector] public bool hasTakenOut;

    private float currentFireRate;
    private float fireRate;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponentInParent<PlayerMovement>();
        audioS = GetComponent<AudioSource>();

        transform.localPosition = hipPos;

        fireRate = weaponData.fireRate;
        currentFireRate = weaponData.fireRate;
    }

    private void Update()
    {
        UpdateAnimation();
        if (weaponData.itemtype == ItemScriptableObject.ItemType.Weapon)
        {

            if (currentFireRate < fireRate)
            {
                currentFireRate += Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.R) && !player.windowhandler.inventory.opened)
            {
                Start_Reload();
            }

            UpdateAiming();

            if (player.windowhandler.inventory.opened)
            {
                return;
            }

                if (isAutomatic)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        if(!weaponData.shotgunFire)
                        {
                            Shoot();
                        }
                        else
                        {
                            ShotgunShoot();
                        }
                    }  
                }
                else
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if(!weaponData.shotgunFire)
                        {
                            Shoot();
                        }
                        else
                        {
                            ShotgunShoot();
                        }
                    }   
                }
        }
        else if (weaponData.itemtype == ItemScriptableObject.ItemType.MeleeWeapon)
        {

            if (currentFireRate < fireRate)
            {
                currentFireRate += Time.deltaTime;
            }
            if (Input.GetButton("Fire1"))
            {
                Swing();
            }

        }
    }

    #region Fire Weapon Function

    public void Shoot()
    {
        if (currentFireRate < fireRate || isReloading || !hasTakenOut || player.running || slotEquippedOn.StackSize <= 0 || player.windowhandler.inventory.opened)
        {
            return;
        }

        GetComponentInParent<Animator>().SetTrigger("Shake");

        RaycastHit hit;

        Vector3 shootDir = shootPoint.forward;
        if (isAiming)
        {
            shootDir.x += Random.Range(-weaponData.aimSpread, weaponData.aimSpread);
            shootDir.y += Random.Range(-weaponData.aimSpread, weaponData.aimSpread);
        }
        else
        {
            shootDir.x += Random.Range(-weaponData.hipSpread, weaponData.hipSpread);
            shootDir.y += Random.Range(-weaponData.hipSpread, weaponData.hipSpread);
        }

        if (Physics.Raycast(shootPoint.position, shootDir, out hit, weaponData.range, shootableLayers))
        {
            GameObject bulletHole = Instantiate(bulletHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            BasicAI ai = hit.transform.GetComponent<BasicAI>();

            if (ai != null)
            {
                ai.health -= weaponData.damage;
            }
            Debug.Log($"Hitted = {hit.transform.name}");
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Stop();
            muzzleFlash.startRotation = Random.Range(0, 360);
            muzzleFlash.Play();
            muzzleFlash.Play();
        }
        else
        {
            Debug.LogWarning("Muzzle flash not assigned");
        }




        anim.CrossFadeInFixedTime("Shoot Base",0.015f);

        GetComponentInParent<CameraLook>().RecoilCamera(Random.Range(-weaponData.horizontalRecoil, weaponData.horizontalRecoil), Random.Range(weaponData.minVerticalRecoil, weaponData.maxVerticalRecoil));

        audioS.PlayOneShot(weaponData.shootSound);

        currentFireRate = 0;

        slotEquippedOn.StackSize--;
        slotEquippedOn.UpdateSlot();
    }

    public void ShotgunShoot()
    {
        if (currentFireRate < fireRate || isReloading || !hasTakenOut || player.running || slotEquippedOn.StackSize <= 0 || player.windowhandler.inventory.opened)
        {
            return;
        }

        GetComponentInParent<Animator>().SetTrigger("Shake");

        for (int i = 0; i < weaponData.pelletPerShot; i++)
        {

            RaycastHit hit;

            Vector3 shootDir = shootPoint.forward;
            if (isAiming)
            {
                shootDir.x += Random.Range(-weaponData.aimSpread, weaponData.aimSpread);
                shootDir.y += Random.Range(-weaponData.aimSpread, weaponData.aimSpread);
            }
            else
            {
                shootDir.x += Random.Range(-weaponData.hipSpread, weaponData.hipSpread);
                shootDir.y += Random.Range(-weaponData.hipSpread, weaponData.hipSpread);
            }

            if (Physics.Raycast(shootPoint.position, shootDir, out hit, weaponData.range, shootableLayers))
            {
                GameObject bulletHole = Instantiate(bulletHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                BasicAI ai = hit.transform.GetComponent<BasicAI>();

                if (ai != null)
                {
                    ai.health -= weaponData.damage;
                }
                
                Debug.Log($"Hitted = {hit.transform.name}");
            }
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Stop();
            muzzleFlash.startRotation = Random.Range(0, 360);
            muzzleFlash.Play();
        }
        else
        {
            Debug.LogWarning("Muzzle flash not assigned");
        }





        anim.CrossFadeInFixedTime("Shoot Base",0.015f);

        GetComponentInParent<CameraLook>().RecoilCamera(Random.Range(-weaponData.horizontalRecoil, weaponData.horizontalRecoil), Random.Range(weaponData.minVerticalRecoil, weaponData.maxVerticalRecoil));

        audioS.PlayOneShot(weaponData.shootSound);

        currentFireRate = 0;

        slotEquippedOn.StackSize--;
        slotEquippedOn.UpdateSlot();
    }

    public void Start_Reload()
    {
        if (isReloading || slotEquippedOn.StackSize >= weaponData.magSize || player.running || !hasTakenOut || !CheckForBullets(weaponData.bulletData, weaponData.magSize))
        //if (isReloading || isAiming || slotEquippedOn.StackSize >= weaponData.magSize || player.running || !hasTakenOut || !CheckForBullets(weaponData.bulletData, weaponData.magSize))
        {
            return;
        }

        audioS.PlayOneShot(weaponData.reloadSound);

        anim.CrossFadeInFixedTime("Reload Base", 0.015f);

        isReloading = true;
    }

    public void Finish_Reload()
    {
        isReloading = false;

        TakeBullets(weaponData.bulletData, weaponData.magSize);
    }


    private bool CheckForBullets(ItemScriptableObject bulletData, int magSize)
    {
        InventoryManager inventory = GetComponentInParent<PlayerMovement>().GetComponentInChildren<InventoryManager>();

        int amountfound = 0;

        for (int b = 0; b < inventory.inventorySlots.Length; b++)
        {
            if (!inventory.inventorySlots[b].IsEmpty)
            {
                if (inventory.inventorySlots[b].data == bulletData)
                {
                    amountfound += inventory.inventorySlots[b].StackSize;
                }
            }
        }


        if (amountfound < 1)
            return false;

        return true;
    }

    public void TakeBullets(ItemScriptableObject bulletData, int magSize)
    {
        InventoryManager inventory = GetComponentInParent<PlayerMovement>().GetComponentInChildren<InventoryManager>();

        int ammoToReload = weaponData.magSize - slotEquippedOn.StackSize;
        int ammoInTheInventory = 0;

        // CHECK FOR THE BULLETS
        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            if (!inventory.inventorySlots[i].IsEmpty)
            {
                if (inventory.inventorySlots[i].data == bulletData)
                {
                    ammoInTheInventory += inventory.inventorySlots[i].StackSize;
                }
            }
        }


        int ammoToTakeFromInventory = (ammoInTheInventory >= ammoToReload) ? ammoToReload : ammoInTheInventory;


        // TAKE THE BULLETS FROM THE INVENTORY
        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            if (!inventory.inventorySlots[i].IsEmpty && ammoToTakeFromInventory > 0)
            {
                if (inventory.inventorySlots[i].data == bulletData)
                {
                    if (inventory.inventorySlots[i].StackSize <= ammoToTakeFromInventory)
                    {
                        slotEquippedOn.StackSize += inventory.inventorySlots[i].StackSize;
                        ammoToTakeFromInventory -= inventory.inventorySlots[i].StackSize;
                        inventory.inventorySlots[i].Clean();
                    }
                    else if (inventory.inventorySlots[i].StackSize > ammoToTakeFromInventory)
                    {
                        slotEquippedOn.StackSize = weaponData.magSize;
                        inventory.inventorySlots[i].StackSize -= ammoToTakeFromInventory;
                        ammoToTakeFromInventory = 0;
                        inventory.inventorySlots[i].UpdateSlot();
                    }
                }
            }
        }


        slotEquippedOn.UpdateSlot();

    }




    public void UpdateAiming()
    {
        if (Input.GetMouseButton(1) && !player.running && !isReloading && !player.windowhandler.inventory.opened)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, aimPos, aimSpeed * Time.deltaTime);
            isAiming = true;
        }
        else if (!isReloading)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, hipPos, aimSpeed * Time.deltaTime);
            isAiming = false;
        }
        //else if
        // {
        //     transform.localPosition = Vector3.Slerp(transform.localPosition, hipPos, aimSpeed * Time.deltaTime);
        //     isAiming = false;
        // }
    }



    #endregion



    #region Melee Function

    public void Swing()
    {
        if (currentFireRate < fireRate || isReloading || !hasTakenOut || player.running || slotEquippedOn.StackSize <= 0 || player.windowhandler.inventory.opened)
        {
            return;
        }

        anim.SetTrigger("Swing");

        currentFireRate = 0;

        CheckForHit();
    }

    public void CheckForHit()
    {
        RaycastHit hit;

        if (Physics.SphereCast(shootPoint.position, 0.2f, shootPoint.forward, out hit, weaponData.range, shootableLayers))
        {
            Hit();
        }
        else
        {
            Miss();
        }

    }

    public void Miss()
    {
        anim.SetTrigger("Miss");
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void ExecuteHit()
    {
        RaycastHit hit;

        GetComponentInParent<Animator>().SetTrigger("Shake");

        if (Physics.SphereCast(shootPoint.position, 0.2f, shootPoint.forward, out hit, weaponData.range, shootableLayers))
        {
            GatherableObject gatherobj = hit.transform.GetComponent<GatherableObject>();
            GatherExtension gatherexten = hit.transform.GetComponent<GatherExtension>();



            if (gatherobj != null)
            {
                gatherobj.Gather(weaponData, GetComponentInParent<WindowHandler>().inventory);
            }
            if (gatherexten != null)
            {
                gatherexten.Gather(weaponData, GetComponentInParent<WindowHandler>().inventory);
            }
        }
    }



    #endregion

    public void UpdateAnimation()
    {
        anim.SetBool("Running", player.running);
    }

    public void Equip(Slot slot)
    {
        gameObject.SetActive(true);

        GetComponentInParent<CameraFOV_Handler>().weapon = this;

        slotEquippedOn = slot;
        slotEquippedOn.weaponEquipped = this;

        transform.localPosition = hipPos;

    }

    public void Unequip()
    {
        GetComponentInParent<CameraFOV_Handler>().weapon = null;

        slotEquippedOn.weaponEquipped = null;
        slotEquippedOn = null;

        isReloading = false;
        hasTakenOut = false;

        gameObject.SetActive(false);
    }
}
