using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxhealth = 100f;
    [Space]
    public float hunger;
    public float maxhunger = 100f;
    [Space]
    public float thirst;
    public float maxthirst = 100f;

    [Space]

    public float HungerDepletion = 0.6f;
    public float ThirstDepletion = 0.75f;

    [Space]

    public float HungerDamage = 1.5f;
    public float ThirstDamage = 1.5f;

    [Space]

    public StatsBar Healthbar;
    public StatsBar Hungerbar;
    public StatsBar Thirstbar;
    public Image redOverlay;

    private void Start()
    {
        health = maxhealth;
        hunger = maxhunger;
        thirst = maxthirst;
    }

    private void Update()
    {
        UpdateStats();
        UpdateUI();
    }

    private void UpdateUI()
    {
        Healthbar.numberText.text = health.ToString("f0");
        Healthbar.bar.fillAmount = health / 100;

        Hungerbar.numberText.text = hunger.ToString("f0");
        Hungerbar.bar.fillAmount = hunger / 100;

        Thirstbar.numberText.text = thirst.ToString("f0");
        Thirstbar.bar.fillAmount = thirst / 100;
    }

    private void UpdateStats()
    {
        if (health <= 0)
        {
            health = 0;
        }
        if (health >= maxhealth)
        {
            health = maxhealth;
        }
        //--------------------------------------------//
        if (hunger <= 0)
        {
            hunger = 0;
        }
        if (hunger >= maxhunger)
        {
            hunger = maxhunger;
        }
        //--------------------------------------------//
        if (thirst <= 0)
        {
            thirst = 0;
        }
        if (thirst >= maxthirst)
        {
            thirst = maxthirst;
        }

        //Player Stats Get Damages
        if (hunger <= 0)
        {
            redOverlay.gameObject.SetActive(true);
            StartCoroutine(FadeOverlayOut());
            health -= HungerDamage * Time.deltaTime;
        }

        if (thirst <= 0)
        {
            redOverlay.gameObject.SetActive(true);
            StartCoroutine(FadeOverlayOut());
            health -= ThirstDamage * Time.deltaTime;
        }

        //Player Stats Depletion
        if (hunger > 0)
        {
            hunger -= HungerDepletion * Time.deltaTime;
        }

        if (thirst > 0)
        {
            thirst -= ThirstDepletion * Time.deltaTime;
        }
    }

    public IEnumerator FadeOverlayOut()
    {
        //Debug.Log("Player got hurt!");
        Color color = redOverlay.color;
        color.a = 1f;
        redOverlay.color = color;

        float fadeDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime/fadeDuration);
            redOverlay.color = color;
            yield return null;
        }

        redOverlay.gameObject.SetActive(false);
        //Debug.Log("Overlay Hidden");
    }
}
