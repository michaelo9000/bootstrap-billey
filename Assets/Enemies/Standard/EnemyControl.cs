﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    public Animator anim;
    public new SpriteRenderer renderer;

    Transform player;
    public bool movingToPlayer;
    public float framesBetweenPositionChecks;
    public float noticePlayerProximity;
    public float enemyLerp;

    public float health;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RepeatingEvents.RegisterMethod(this, "CheckDistanceToPlayer", 30, 0);
    }
    
    void Update ()
    {
        if (movingToPlayer)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(player.position.x, transform.position.y), 1 / enemyLerp);
            renderer.flipX = transform.position.x - player.position.x > 0;
        }
	}

    void CheckDistanceToPlayer()
    {
        var grad = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        var dist = Mathf.Sqrt(grad.x * grad.x + grad.y * grad.y);
        if (dist < noticePlayerProximity)
            movingToPlayer = true;
        else
            movingToPlayer = false;
        anim.SetBool("follow", movingToPlayer);
    }
}
