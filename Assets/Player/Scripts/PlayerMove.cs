using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    PlayerReferences References;
    public enum Stature { standing, ducked, rolling, dropped }
    public Stature stature = Stature.standing;
    private void Start()
    {
        References = GetComponent<PlayerReferences>();
    }
    public float moveSpeed;
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
    }    
    public int rollFrames;
    public float rollSpeedModConst;
    float? rollSpeedMod;
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
        ModifyStature(Stature.rolling);
        StartCoroutine(IRoll());
    }
    IEnumerator IRoll()
    {
        var frames = 0;
        while (frames < rollFrames)
        {
            frames++;
            // Needs to be based on 1, as it is a multiplier.
            rollSpeedMod = 1 + ( rollSpeedModConst - rollSpeedModConst * frames / rollFrames );
            yield return References.FrameWait;
        }
        rollSpeedMod = null;
        StandUp();
    }
    public void StandUp()
    {
        ModifyStature(Stature.standing);
    }
    void ModifyStature(Stature newStature)
    {
        stature = newStature;
        float sizeModY;
        Color color;
        switch (stature)
        {
            case Stature.dropped:
                sizeModY = 4;
                color = Color.red;
                break;
            case Stature.ducked:
                sizeModY = 2;
                color = Color.yellow;
                break;
            case Stature.rolling:
                sizeModY = 1.5f;
                color = Color.cyan;
                break;
            case Stature.standing:
                sizeModY = 1;
                color = Color.white;
                break;
            default: 
                sizeModY = 1;
                color = Color.white;
                break;
        }
        // var newSize = new Vector2(References._Collider2D.size.x, References.ColliderStartSize.y/sizeModY);
        // References._Collider2D.offset = new Vector2 (
        //     References._Collider2D.offset.x, 
        //     -(References.ColliderStartSize.y - (newSize.y < newSize.x? newSize.x:newSize.y))/2
        // );
        // References._Collider2D.size = newSize;
        References._SpriteRenderer.color = color;
    }

    public int jumpForce;
    public bool grounded;
    public void Jump()
    {
        if (!grounded)
            return;
        References._Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}