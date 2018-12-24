using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionKeeper : MonoBehaviour {

    private class Collision
    {
        // TODO keeping gameobjects not the best maybe? IDs?
		// Nah need the object to access the health methods.
        public GameObject gameObject { get; set; }
        public bool isColliding { get; set; }
        public bool isEnemy { get; set; }
        public float healthHeld { get; set; }
        public Collision(GameObject obj, bool _isEnemy)
        {
            gameObject = obj;
            isColliding = true;
			isEnemy = _isEnemy;
            healthHeld = 0;
        }
    }
    private List<Collision> Collisions = new List<Collision>();
    int IndexOfCollision(GameObject obj)
    {
        for (int i = 0; i < Collisions.Count; i++)
        {
            if (Collisions[i].gameObject == obj)
            {
                return i;
            }
        }
        return -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
		var obj = collision.gameObject;
        var i = IndexOfCollision(obj);
        if(i > -1)
        {
            Collisions[i].isColliding = true;
        }         
        else
        {
            Collisions.Add(new Collision(obj, obj.GetComponent<HealthAndDamage>() != null));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)    
    {
        var i = IndexOfCollision(collision.gameObject);
        if(i > -1)
        {
            Collisions[i].isColliding = false;
        }
    }
    public void UpdateHealthHeld(GameObject obj, float damage)
    {
        var i = IndexOfCollision(obj);
        if(i > -1)
        {
            Collisions[i].healthHeld += damage;
            Debug.Log(this.gameObject.name + " says "+obj.name+" has "+Collisions[i].healthHeld + " of my HP!");
        }
        else 
        {
			// Surely this wouldn't happen if OnTriggerEnter occurs first? (which it should)
			Debug.Log("UNEXPECTED: New collision created for " + obj.name + " in UpdateHealthHeld");
            Collisions.Add(new Collision(obj, true));
        }
    }
    public float RemoveHealthHeld(GameObject obj)
    {
        var i = IndexOfCollision(obj);
        if(i > -1)
        {
            var healthReturned = Collisions[i].healthHeld;
            Collisions[i].healthHeld = 0;
            return healthReturned;
        }
        return 0;
    }
	public float DamageAllEnemyCollisions(float damage)
	{
		float healthToRestore = 0;
		var enemies = Collisions.Where(c => c.isEnemy == true).Select(c => c).ToArray();
        for (int i = 0; i < enemies.Length; i++)
        {            
            if (enemies[i].isColliding){
                var didKill = enemies[i].gameObject.GetComponent<HealthAndDamage>().TakeDamage(damage, gameObject);
                if (didKill)
                {
                    healthToRestore += RemoveHealthHeld(enemies[i].gameObject);
                }
            }
        }
		return healthToRestore;
	}
}
