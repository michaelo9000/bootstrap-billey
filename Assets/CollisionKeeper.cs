using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionKeeper : MonoBehaviour {

    // Perfectly acceptable for this to be null - that just means that this game object does not do damage.
    DoesDamage _DoesDamage;

    public class Collision
    {
        public GameObject gameObject { get; set; }
        public bool isColliding { get; set; }
        public bool isEnemy { get; set; }
        public bool isPlatform { get; set; }
        public float healthHeld { get; set; }
        public Collision(GameObject obj, bool _isEnemy, bool _isPlatform)
        {
            gameObject = obj;
            isColliding = true;
			isEnemy = _isEnemy;
			isPlatform = _isPlatform;
            healthHeld = 0;
        }
    }

    private void Start()
    {
        _DoesDamage = GetComponent<DoesDamage>();
    }

    private List<Collision> Collisions = new List<Collision>();

    private int GetIndexOfCollision(GameObject obj)
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

    public Collision GetCollisionWithGameObject(GameObject obj)
    {
        for (int i = 0; i < Collisions.Count; i++)
        {
            if (Collisions[i].gameObject == obj)
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

    public void UpdateCollisionWithGameObject(GameObject obj, Collision updatedCollision)
    {
        var index = GetIndexOfCollision(obj);
        Collisions[index] = updatedCollision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collision storedCollision;

        var obj = collision.gameObject;
        var index = GetIndexOfCollision(obj);

        if (index > -1)
        {
            storedCollision = Collisions[index];
            Collisions[index].isColliding = true;
        }
        else
        {
            storedCollision = new Collision(obj, obj.GetComponent<HealthAndDamage>() != null, true);
            Collisions.Add(storedCollision);
        }
        if (_DoesDamage != null)
            _DoesDamage.DamageCollision(storedCollision);
    }

    private void OnTriggerExit2D(Collider2D collision)    
    {
        var i = GetIndexOfCollision(collision.gameObject);
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
