using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour 
{
    EnemyReferences References;
    public float moveSpeed;
    public int jumpForce;

    private void Start()
    { 
        References = GetComponent<EnemyReferences>();
    }

    public void Move(float h)
    {
        // Define enemy's velocity 
        References._Rigidbody2D.velocity = new Vector2(h * moveSpeed, References._Rigidbody2D.velocity.y);
        // Check if the enemy's facing direction needs to be flipped
        if (Mathf.Sign(h) != transform.localScale.x)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);

        References._Surrogate.SetPosition();
    }

    public void Jump()
    {
        if (!References._CollisionKeeper.CanJump())
            return;
        References._Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}