using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour{
    
    private class HasMyHealthObject
    {
        //TODO keeping gameobjects not the best maybe? IDs?
        public GameObject gameObject { get; set; }
        public float healthHeld { get; set; }
        public HasMyHealthObject(GameObject obj, float _healthHeld)
        {
            gameObject = obj;
            healthHeld = _healthHeld;
        }
    }
    private List<HasMyHealthObject> healthHolders = new List<HasMyHealthObject>();
    public void AddOrUpdateHasMyHealthObject(GameObject obj, float damage)
    {
        for (int i = 0; i < healthHolders.Count; i++)
        {
            if (healthHolders[i].gameObject == obj)
            {
                healthHolders[i].healthHeld += damage;
                Debug.Log("update:"+obj.name+"damage:"+healthHolders[i].healthHeld);
                return;
            }
        }
        Debug.Log("add:"+obj.name+"damage:"+damage);
        healthHolders.Add(new HasMyHealthObject(obj, damage));
    }
    public float RemoveHasMyHealthObject(GameObject obj)
    {
        for(int i = 0; i < healthHolders.Count; i++)
        {
            if (healthHolders[i].gameObject == obj)
            {
                var healthReturned = healthHolders[i].healthHeld;
                healthHolders.RemoveAt(i);
                return healthReturned;
            }
        }
        return 0;
    }
}