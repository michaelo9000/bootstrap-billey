using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemyPrefab;
    GameObject myBaby;

    private void Start()
    {
        RepeatingEvents.RegisterMethod(this, "SpawnEnemy", 180, 0);
    }

    public void SpawnEnemy()
    {
        if(myBaby == null)
            myBaby = Instantiate(enemyPrefab, gameObject.transform);
    }
}