using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmOnTouch : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SendMessage("HandleDeath");
    }
}