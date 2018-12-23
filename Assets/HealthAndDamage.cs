using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndDamage : MonoBehaviour {
    
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
    void UpdateHealthAndStamina(float healthAmount, float staminaAmount)
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
        AddOrUpdateHealthHeld(HitMeObject.transform.root.gameObject, amount);
        return null;
    }
    //TODO how useful is this? Do I want to damage every colliding object every time I do damage?
    public float DamageCollisions(float damageAmount)
    {
        float healthToRestore = 0;
        for (int i = 0; i < MyEnemies.Count; i++)
        {            
            if (MyEnemies[i].isColliding){
                var didKill = MyEnemies[i].gameObject.GetComponent<HealthAndDamage>().TakeDamage(damageAmount, gameObject);
                if (didKill)
                {
                    healthToRestore += RemoveHealthHeld(MyEnemies[i].gameObject);
                }
            }
        }
        UpdateHealthAndStamina(healthToRestore, 0);
        return healthToRestore;
    }
    private class MyEnemy
    {
        //TODO keeping gameobjects not the best maybe? IDs?
        public GameObject gameObject { get; set; }
        public float healthHeld { get; set; }
        public bool isColliding { get; set; }
        public MyEnemy(GameObject obj, float _healthHeld, bool _isColliding)
        {
            gameObject = obj;
            healthHeld = _healthHeld;
            isColliding = _isColliding;
        }
    }
    private List<MyEnemy> MyEnemies = new List<MyEnemy>();
    public void AddOrUpdateHealthHeld(GameObject obj, float damage)
    {
        var i = IndexOfEnemyInList(obj);
        if(i > -1)
        {
            MyEnemies[i].healthHeld += damage;
            Debug.Log(this.gameObject.name + " says "+obj.name+" has "+MyEnemies[i].healthHeld + " of my HP!");
        }
        else 
        {
            MyEnemies.Add(new MyEnemy(obj, damage, false));
        }
    }
    public float RemoveHealthHeld(GameObject obj)
    {
        var i = IndexOfEnemyInList(obj);
        if(i > -1)
        {
            var healthReturned = MyEnemies[i].healthHeld;
            MyEnemies[i].healthHeld = 0;
            return healthReturned;
        }
        return 0;
    }
    public void AddOrUpdateCollision(GameObject obj)
    {
        var i = IndexOfEnemyInList(obj);
        if(i > -1)
        {
            MyEnemies[i].isColliding = true;
        }         
        else if (obj.GetComponent<HealthAndDamage>())
        {
            MyEnemies.Add(new MyEnemy(obj, 0, true));
        }
    }
    public void RemoveCollision(GameObject obj)
    {
        var i = IndexOfEnemyInList(obj);
        if(i > -1)
        {
            MyEnemies[i].isColliding = false;
        }
    }
    int IndexOfEnemyInList(GameObject obj)
    {
        for (int i = 0; i < MyEnemies.Count; i++)
        {
            if (MyEnemies[i].gameObject == obj)
            {
                return i;
            }
        }
        return -1;
    }
}