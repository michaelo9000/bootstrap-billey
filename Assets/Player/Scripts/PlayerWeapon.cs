using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour{

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
