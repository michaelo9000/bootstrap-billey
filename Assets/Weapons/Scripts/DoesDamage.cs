using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour{

    public GameObject Owner;
    SentientReferences OwnerReferences;
    CollisionKeeper _CollisionKeeper;
    public float damage = 5;

    void Start()
    {
        OwnerReferences = Owner.GetComponent<SentientReferences>();
        _CollisionKeeper = GetComponent<CollisionKeeper>();
    }

    public void DamageCollision(CollisionKeeper.Collision collision)
    {
        // Don't damage yourself, fool
        if (collision.gameObject.GetInstanceID() == Owner.GetInstanceID())
            return;

        float healthToRestore = 0;
        if (collision.isEnemy)
        {
            var didKill = collision.gameObject.GetComponent<HealthAndDamage>().TakeDamage(damage, Owner);
            if (didKill)
            {
                healthToRestore += OwnerReferences._HealthAndDamage.RemoveHealthHeld(collision.gameObject);
            }
        }
        OwnerReferences._HealthAndDamage.UpdateHealthAndStamina(healthToRestore, 0);
    }
}
