using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour{
        
    private class CollisionObject
    {
        //TODO keeping gameobjects not the best maybe? IDs?
        public GameObject gameObject { get; set; }
        public bool damageable { get; set; }
        public CollisionObject(GameObject obj)
        {
            gameObject = obj;
            damageable = obj.GetComponent<SentientBeing>();
        }
    }    
    private List<CollisionObject> currentCollisions = new List<CollisionObject>();
    public void AddCollisionObject(GameObject obj)
    {
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].gameObject == obj)
                return;
        }
        currentCollisions.Add(new CollisionObject(obj));
    }
    public void RemoveCollisionObject(GameObject obj)
    {
        for(int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].gameObject == obj)
            {
                currentCollisions.RemoveAt(i);
                return;
            }
        }
    }    
    public List<GameObject> DamageCollisions(float damageAmount)
    {
        var killedGuys = new List<GameObject>();
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            var guy = currentCollisions[i];
            if (guy.damageable)
            {
                var killed = guy.gameObject.GetComponent<SentientBeing>().TakeDamage(damageAmount, gameObject);
                if (killed)
                {
                    killedGuys.Add(killed);
                }
            }
        }
        return killedGuys;
    }
}