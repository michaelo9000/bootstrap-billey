using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSquare : MonoBehaviour{

    SentientReferences References;
    public Color idleColor;
    public Color activeColor;

    private void Start()
    {
        References = GetComponent<SentientReferences>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        References._SpriteRenderer.color = activeColor;
        var healthToRestore = References._CollisionKeeper.DamageAllEnemyCollisions(5);
        References._HealthAndDamage.UpdateHealthAndStamina(healthToRestore, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        References._SpriteRenderer.color = idleColor;
    }
}
