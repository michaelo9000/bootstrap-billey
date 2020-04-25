using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionKeeper : MonoBehaviour {

    // Perfectly acceptable for this to be null - that just means that this game object does not do damage.
    Weapon _DoesDamage;

    public class Collision
    {
        public int instanceId { get; set; }
        public bool isColliding { get; set; }
        public bool isDamageable { get; set; }
        public bool isPlatform { get; set; }
        public Collision(int id, bool _isDamageable, bool _isPlatform)
        {
            instanceId = id;
            isColliding = true;
			isDamageable = _isDamageable;
			isPlatform = _isPlatform;
        }
    }

    private void Start()
    {
        _DoesDamage = GetComponent<Weapon>();
    }

    private List<Collision> Collisions = new List<Collision>();

    private int GetIndexOfCollision(int id)
    {
        for (int i = 0; i < Collisions.Count; i++)
        {
            if (Collisions[i].instanceId == id)
            {
                return i;
            }
        }
        return -1;
    }

    public Collision GetCollisionFromID(int id)
    {
        for (int i = 0; i < Collisions.Count; i++)
        {
            if (Collisions[i].instanceId == id)
            {
                return Collisions[i];
            }
        }
        return null;
    }

    public Collision GetCollisionWithIndex(int index)
    {
        return Collisions[index];
    }

    // This has to be the only place that collisions are handled for every object, or we get weird timing results.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collision storedCollision;

        var id = collision.gameObject.GetInstanceID();
        var index = GetIndexOfCollision(id);

        if (index > -1)
        {
            storedCollision = Collisions[index];
            Collisions[index].isColliding = true;
        }
        else
        {
            storedCollision = new Collision
            (
                id, 
                collision.gameObject.GetComponent<HealthManager>() != null, 
                // Everything is a platform??
                true
            );
            Collisions.Add(storedCollision);
        }

        if (_DoesDamage != null && storedCollision.isDamageable)
            _DoesDamage.DamageCollision(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)    
    {
        var i = GetIndexOfCollision(collision.gameObject.GetInstanceID());
        if(i > -1)
        {
            Collisions[i].isColliding = false;
        }
    }

    public bool CanJump()
    {
        for(int i = 0; i < Collisions.Count; i++)
        {
            if(Collisions[i].isPlatform && Collisions[i].isColliding)
                return true;
        }
        return false;
    }
}
