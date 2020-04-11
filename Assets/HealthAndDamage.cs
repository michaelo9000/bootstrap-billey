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
        UpdateHealthHeld(HitMeObject.transform.root.gameObject, amount);
        return null;
    }
    
    public void UpdateHealthHeld(GameObject obj, float damage)
    {
        var collision = References._CollisionKeeper.GetCollisionWithGameObject(obj);

        if (collision != null)
        {
            collision.healthHeld += damage;
            Debug.Log($"{gameObject.name} says: {obj.name} has {collision.healthHeld} of my HP!");
        }
        else
        {
            Debug.Log($"UNEXPECTED: Previously unknown collision ({obj.name}) did damage to {gameObject.name}.");
            // Surely this wouldn't happen if OnTriggerEnter occurs first? (which it should)
            //Debug.Log("UNEXPECTED: New collision created for " + obj.name + " in UpdateHealthHeld");
            //References._CollisionKeeper.Collisions.Add(new CollisionKeeper.Collision(obj, true, true));
        }
    }
    
    public float RemoveHealthHeld(GameObject obj)
    {
        var collision = References._CollisionKeeper.GetCollisionWithGameObject(obj);
        if (collision != null)
        {
            var healthReturned = collision.healthHeld;
            collision.healthHeld = 0;
            References._CollisionKeeper.UpdateCollisionWithGameObject(obj, collision);
            return healthReturned;
        }
        return 0;
    }
}