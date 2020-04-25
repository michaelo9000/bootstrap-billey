using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageManager : MonoBehaviour
{
    SentientReferences References;
    public bool isInvulnerable = false;

    private class ClashedEnemy
    {
        public int Id { get; set; }
        public GameObject GameObject { get; set; }
        public float HealthLost { get; set; }
        public float HealthTaken { get; set; }
        public bool Dead { get; set; }
    }

    private List<ClashedEnemy> ClashedEnemies = new List<ClashedEnemy>();

    void Start()
    {
        References = GetComponent<SentientReferences>();
    }

    private ClashedEnemy GetOrAddClashedEnemy(GameObject obj, bool dead = false)
    {
        var enemyId = obj.GetInstanceID();
        var storedEnemy = ClashedEnemies.FirstOrDefault(e => e.Id == enemyId);
        if (storedEnemy == null)
        {
            storedEnemy = new ClashedEnemy() { Id = enemyId, GameObject = obj, Dead = dead };
            ClashedEnemies.Add(storedEnemy);
        }
        return storedEnemy;
    }

    public void DealDamage(float amount, GameObject enemyObj)
    {
        var enemy = GetOrAddClashedEnemy(enemyObj);
        // Need to ensure player can be damaged after respawning.
        if (enemy.Dead)
            return;

        var damageDealt = enemyObj.GetComponent<DamageManager>().TakeDamage(amount, gameObject);

        enemy.HealthTaken += damageDealt;
    }

    public float TakeDamage(float amount, GameObject enemyObj)
    {
        var enemy = GetOrAddClashedEnemy(enemyObj);

        if (isInvulnerable)
            return 0;

        References._HealthManager.UpdateHealth(-amount);
        enemy.HealthLost += amount;
        // Return the amount of damage actually taken
        return amount;
    }

    public void ReceiveObituary(GameObject enemyObj)
    {
        var enemy = GetOrAddClashedEnemy(enemyObj, true);
        enemy.Dead = true;
        References._HealthManager.UpdateHealth(enemy.HealthLost);
        enemy.HealthLost = 0;
    }

    public void HandleDeath()
    {
        foreach (var enemy in ClashedEnemies.Where(i => !i.Dead))
        {
            enemy.GameObject.GetComponent<DamageManager>().ReceiveObituary(gameObject);
        }
    }
}
