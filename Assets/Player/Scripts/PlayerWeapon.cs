using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour{

    // Need a better way todo this, so that the weapon accesses the player's References script
    PlayerReferences References;
    void Start()
    {
        References = GetComponent<PlayerReferences>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthToRestore = References._CollisionKeeper.DamageAllEnemyCollisions(5);
        References._HealthAndDamage.UpdateHealthAndStamina(healthToRestore, 0);
    }
}
