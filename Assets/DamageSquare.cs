using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSquare : SentientBeing{

    public SpriteRenderer sprite;
    public Color idleColor;
    public Color activeColor;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddOrUpdateCollision(collision.gameObject);
        sprite.color = activeColor;
        if(collision.GetComponent<SentientBeing>())
        {
            DamageCollisions(5);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveCollision(collision.gameObject);
        sprite.color = idleColor;
    }
}
