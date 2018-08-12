using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Interactor {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddCollision(collision.gameObject);
        DamageCollisions(5);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveCollision(collision.gameObject);
    }
}
