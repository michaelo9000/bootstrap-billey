using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    EnemyReferences References;
    public bool movingToPlayer;
    public bool inAttackRange;
    public float framesBetweenPositionChecks;
    public float noticePlayerProximity;
    public float playerCloseEnoughDistance;
    public float playerDistance;
    public float jumpToPlayerDistance;

    void Start () 
    {
        References = GetComponent<EnemyReferences>();
        RepeatingEvents.RegisterMethod(this, "CheckDistanceToPlayer", 10, 0);
        References.CurrentWeapon = References.HeldWeapons[0];
    }
    
    void FixedUpdate ()
    {
        if (movingToPlayer)
        {
            var playerIsInFront = References._Rigidbody2D.position.x < StaticGlobalReferences._Player.transform.position.x;
            References._EnemyMove.Move(playerIsInFront ? 1 : -1);
            // Add another check for player vertical velocity, to determine whether the player is at the start of a jump that will take them outside the jumpToPlayerDistance
            var playerIsHighUp = StaticGlobalReferences._Player.transform.position.y - References._Rigidbody2D.position.y > jumpToPlayerDistance;
            if (playerIsHighUp)
                References._EnemyMove.Jump();
        }
        if (inAttackRange)
        {
            if (!References._AttackManager.WindingUp && !References._AttackManager.Attacking)
                References._AttackManager.DoAttack();
        }
        References._Surrogate.SetPosition();
    }

    public void HandleDeath()
    {
        Destroy(References._Surrogate.gameObject);
        Destroy(gameObject);
    }

    void CheckDistanceToPlayer()
    {
        var grad = new Vector2(StaticGlobalReferences._Player.transform.position.x - transform.position.x, StaticGlobalReferences._Player.transform.position.y - transform.position.y);
        playerDistance = Mathf.Sqrt(grad.x * grad.x + grad.y * grad.y);

        inAttackRange = false;
        if (playerDistance <= playerCloseEnoughDistance)
            inAttackRange = true;

        movingToPlayer = false;
        if (playerDistance < noticePlayerProximity && !inAttackRange)
            movingToPlayer = true;
    }
}
