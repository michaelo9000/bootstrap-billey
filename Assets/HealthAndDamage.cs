using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndDamage : MonoBehaviour {
    
    SentientReferences References;
    public float health;
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
        UpdateHealthAndStamina(0,0);
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
        UpdateHealthAndStamina(-staminaCost, staminaCost);
        if(restoringStamina != null) StopCoroutine(restoringStamina);
        restoringStamina = StartCoroutine(RestoreStamina(-staminaCost));
        return true;
    }
    public void UpdateHealthAndStamina(float healthAmount, float staminaAmount)
    {
        health += healthAmount;
        usedStamina += staminaAmount;
        if(healthHUD) healthHUD.text = health.ToString();
        if(staminaHUD) staminaHUD.text = usedStamina.ToString();
    }
    IEnumerator RestoreStamina(float amount)
    {
        yield return waitStaminaRestoreDelay;
        while(usedStamina > 0){
            UpdateHealthAndStamina(1, -1);
            yield return waitStaminaRestoreRate;
        }
    }
    public GameObject TakeDamage(float amount, GameObject HitMeObject)
    {
        UpdateHealthAndStamina(-amount, 0);
        if (health <= 0)
        {
            // Destroy(gameObject);
            return gameObject;
        }
        //TODO rather than root, find the object with the SentientBeing component
        References._CollisionKeeper.UpdateHealthHeld(HitMeObject.transform.root.gameObject, amount);
        return null;
    }
}