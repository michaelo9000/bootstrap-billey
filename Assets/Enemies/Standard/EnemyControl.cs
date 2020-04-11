using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    EnemyReferences References;
    public bool movingToPlayer;
    public float framesBetweenPositionChecks;
    public float noticePlayerProximity;
    public float jumpToPlayerDistance;

    void Start () 
    {
        References = GetComponent<EnemyReferences>();
        RepeatingEvents.RegisterMethod(this, "CheckDistanceToPlayer", 30, 0);
    }
    
    void FixedUpdate ()
    {
        if (movingToPlayer)
        {
            var playerIsInFront = References._Rigidbody2D.position.x < References._Global._Player.position.x;
            References._EnemyMove.Move(playerIsInFront ? 1 : -1);
            // Add another check for player vertical velocity, to determine whether the player is at the start of a jump that will take them outside the jumpToPlayerDistance
            var playerIsHighUp = References._Global._Player.position.y - References._Rigidbody2D.position.y > jumpToPlayerDistance;
            if (playerIsHighUp)
                References._EnemyMove.Jump();
            if (Time.frameCount % 100 == 0)
                References._EnemyAttack.DoAttack();
        }
	}

    void CheckDistanceToPlayer()
    {
        var grad = new Vector2(References._Global._Player.position.x - transform.position.x, References._Global._Player.position.y - transform.position.y);
        var dist = Mathf.Sqrt(grad.x * grad.x + grad.y * grad.y);
        if (dist < noticePlayerProximity)
            movingToPlayer = true;
        else
            movingToPlayer = false;
        //anim.SetBool("follow", movingToPlayer);
    }
}
