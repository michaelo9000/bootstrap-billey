using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSquare : Interactor{

    public SpriteRenderer sprite;
    public Color idleColor;
    public Color activeColor;
    public int totalFlashes;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddCollision(collision.gameObject);
        sprite.color = activeColor;
        DamageCollisions(5);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveCollision(collision.gameObject);
        sprite.color = idleColor;
    }
}
