using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour{

    HealthAndDamage player;
    void Start()
    {
        player = transform.root.gameObject.GetComponent<HealthAndDamage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.AddOrUpdateCollision(collision.gameObject);
        player.DamageCollisions(5);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.RemoveCollision(collision.gameObject);
    }
}
