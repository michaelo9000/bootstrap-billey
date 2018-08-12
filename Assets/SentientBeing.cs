using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentientBeing : MonoBehaviour {
    
    public float health;
    float minHealth;
    public Text healthHUD;

    private void Start()
    {
        minHealth = health * .2f;
        healthHUD.text = health.ToString();
    }

    public bool DoAction(float staminaCost)
    {
        if (health - staminaCost < minHealth)
            return false;
        health -= staminaCost;
        return true;
    }

    public GameObject TakeDamage(float amount)
    {
        health -= amount;
        healthHUD.text = health.ToString();
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " says: dead.");
            Destroy(gameObject);
            return gameObject;
        }
        return null;
    }
}