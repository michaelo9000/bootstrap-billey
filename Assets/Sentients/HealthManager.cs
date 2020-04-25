using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    
    SentientReferences References;
    public float maxHealth;
    float health;
    float minHealth;
    float usedStamina;
    public Text healthHUD;
    public Text staminaHUD;
    Coroutine restoringStamina;
    public float staminaRestoreDelay;
    WaitForSeconds waitStaminaRestoreDelay;
    public float staminaRestoreRate;
    WaitForSeconds waitStaminaRestoreRate;

    private void Start()
    {
        References = GetComponent<SentientReferences>();
        minHealth = health * .2f;
        // Just to get values to display
        UpdateHealth(maxHealth);
        UpdateStamina(0);
        waitStaminaRestoreDelay = new WaitForSeconds(staminaRestoreDelay);
        waitStaminaRestoreRate = new WaitForSeconds(staminaRestoreRate);
    }
    public bool TestStaminaAction(float staminaCost)
    {
        return (health - staminaCost > minHealth);
    }
    public bool DoStaminaAction(float staminaCost)
    {
        if (health - staminaCost < minHealth)
            return false;

        UpdateHealth(-staminaCost);
        UpdateStamina(staminaCost);
        if (restoringStamina != null) 
            StopCoroutine(restoringStamina);

        restoringStamina = StartCoroutine(RestoreStamina());
        return true;
    }
    public float UpdateHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
        if (healthHUD) 
            healthHUD.text = health.ToString();
        if (health <= 0)
            SendMessage("HandleDeath");
        return health;
    }

    public float UpdateStamina(float amount)
    {
        usedStamina += amount;
        if (staminaHUD) 
            staminaHUD.text = usedStamina.ToString();
        return usedStamina;
    }

    IEnumerator RestoreStamina()
    {
        yield return waitStaminaRestoreDelay;
        while (usedStamina > 0)
        {
            UpdateHealth(1);
            UpdateStamina(-1);
            yield return waitStaminaRestoreRate;
        }
    }
}