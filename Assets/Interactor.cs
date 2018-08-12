using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour{
        
    private class CollisionObject
    {
        public GameObject gameObject { get; set; }
        public bool Damageable { get; set; }
        public CollisionObject(GameObject obj)
        {
            gameObject = obj;
            Damageable = obj.GetComponent<SentientBeing>();
        }
    }

    private class DamagedObject
    {
        public GameObject gameObject { get; set; }
        public float damage { get; set; }
        public DamagedObject(GameObject obj)
        {
            gameObject = obj;
            damage = 0;
        }
    }

    private List<CollisionObject> currentCollisions = new List<CollisionObject>();
    private List<DamagedObject> damagedObjects = new List<DamagedObject>();
    
    public void AddCollision(GameObject obj)
    {
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].gameObject == obj)
                return;
        }
        currentCollisions.Add(new CollisionObject(obj));
    }

    public void RemoveCollision(GameObject obj)
    {
        for(int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].gameObject == obj)
            {
                currentCollisions.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// Returns restorable damage from objects killed
    /// </summary>
    /// <param name="damageAmount"></param>
    /// <returns></returns>
    public List<GameObject> DamageCollisions(float damageAmount)
    {
        var killedGuys = new List<GameObject>();
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            var guy = currentCollisions[i];
            if (guy.Damageable)
            {
                var killed = guy.gameObject.GetComponent<SentientBeing>().TakeDamage(damageAmount);
                if (killed)
                {
                    killedGuys.Add(killed);
                }
            }
        }
        return killedGuys;
    }
}