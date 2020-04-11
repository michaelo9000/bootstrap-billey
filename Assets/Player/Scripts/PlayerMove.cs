using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour 
{
    PlayerReferences References;

    public float moveSpeed;

    public enum Stature { standing, ducked, rolling, dropped }
    public Stature stature = Stature.standing;

    public int rollFrames;
    public float rollSpeedModConst;
    float? rollSpeedMod;

    public int jumpForce;

    private void Start()
    {
        References = GetComponent<PlayerReferences>();
    }

    public void Move(float h)
    {
        // If the player is rolling, set 'h' to its max value
        if (stature == Stature.rolling) 
            h = 1 * transform.localScale.x;
        // Define player's velocity 
        if(h != 0)
        {
            var moveSpeedMod = stature == Stature.ducked || stature == Stature.dropped ? .2f : 1;
            References._Rigidbody2D.velocity = new Vector2(h * moveSpeed * moveSpeedMod * (rollSpeedMod ?? 1), References._Rigidbody2D.velocity.y);
        }
        // Check if the player's facing direction needs to be flipped
        if (h != 0 && Mathf.Sign(h) != transform.localScale.x)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);

        References._Surrogate.SetPosition();
    }

    public void DuckEvade()
    {
        ModifyStature(Stature.ducked);
    }

    public void DropEvade()
    {
        ModifyStature(Stature.dropped);
    }

    // Todo this should abort if it gets called while the player is already rolling
    public void RollEvade()
    {
        //if(stature == Stature.rolling)
        //    return;
        ModifyStature(Stature.rolling);
        StartCoroutine(IRoll());
    }
    IEnumerator IRoll()
    {
        var frames = 0;
        while (frames < rollFrames)
        {
            frames++;
            // Has to be based on 1, as it is a multiplier.
            rollSpeedMod = 1 + ( rollSpeedModConst - rollSpeedModConst * frames / rollFrames );
            yield return References.FrameWait;
        }
        rollSpeedMod = null;
        if (Input.GetButton("Evade"))
            ModifyStature(Stature.ducked);
        else if (stature != Stature.dropped)
            ModifyStature(Stature.standing);
    }
    public void StandUp()
    {
        ModifyStature(Stature.standing);
    }
    void ModifyStature(Stature newStature)
    {
        stature = newStature;
        DisableColliders();
        switch (stature)
        {
            case Stature.dropped:
                References._SpriteRenderer.sprite = References.DropSprite;
                References._SpriteRenderer.color = Color.red;
                References.DropCollider.enabled = true;
                break;
            case Stature.ducked:
                References._SpriteRenderer.sprite = References.DuckSprite;
                References._SpriteRenderer.color = Color.yellow;
                References.DuckCollider.enabled = true;
                break;
            case Stature.rolling:
                References._SpriteRenderer.sprite = References.RollSprite;
                References._SpriteRenderer.color = Color.cyan;
                References.RollCollider.enabled = true;
                break;
            case Stature.standing:
            default: 
                References._SpriteRenderer.sprite = References._Sprite;
                References._SpriteRenderer.color = Color.white;
                References._Collider2D.enabled = true;
                break;
        }
    }

    public void Jump()
    {
        if (!References._CollisionKeeper.CanJump())
            return;
        References._Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void DisableColliders()
    {
        References._Collider2D.enabled = false;
        References.DuckCollider.enabled = false;
        References.DropCollider.enabled = false;
        References.RollCollider.enabled = false;
    }
}