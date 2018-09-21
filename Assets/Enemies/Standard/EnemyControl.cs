using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    public Animator anim;
    public new SpriteRenderer renderer;
    Transform player;
    public bool movingToPlayer;
    public float framesBetweenPositionChecks;
    public float noticePlayerProximity;
    public float movementLerp;
    Rigidbody2D myBody;

    void Start () {
        myBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RepeatingEvents.RegisterMethod(this, "CheckDistanceToPlayer", 30, 0);
    }
    
    void FixedUpdate ()
    {
        if (movingToPlayer)
        {   
            //get difference between me and player
            var newPos = new Vector2(
                player.position.x - myBody.position.x,
                player.position.y - myBody.position.y
            );
            myBody.MovePosition(myBody.position + newPos * Time.deltaTime * movementLerp);
            renderer.flipX = myBody.position.x - player.position.x > 0;
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
