using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public enum Stature
    {
        standing,
        ducked,
        rolling,
        dropped
    }
    public Rigidbody2D myBody;
    public CapsuleCollider2D myCollider;
    Vector2 myColliderStartSize;
    public SpriteRenderer mySprite;
    Vector2 mySpriteStartSize;
    SentientBeing _SentientBeing;
    public float moveSpeed;
    public int jumpForce;
    public float rollSpeedModConst;
    float? rollSpeedMod;
    public int rollFrames;
    public static bool grounded;
    public Stature stature = Stature.standing;
    WaitForEndOfFrame frameWait = new WaitForEndOfFrame();
    private void Start()
    {
        _SentientBeing = gameObject.GetComponent<SentientBeing>();
        myColliderStartSize = myCollider.size;
        mySpriteStartSize = mySprite.size;
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
            myBody.velocity = new Vector2(h * moveSpeed * moveSpeedMod * (rollSpeedMod ?? 1), myBody.velocity.y);
        }
        // Check if the player's facing direction needs to be flipped
        if (h != 0 && Mathf.Sign(h) != transform.localScale.x)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
    }    
    public void DuckEvade()
    {
        ModifyStature(Stature.ducked);
    }
    public void DropEvade()
    {
        ModifyStature(Stature.dropped);
    }
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
            yield return frameWait;
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
        myCollider.size = new Vector2(myCollider.size.x, myColliderStartSize.y/sizeModY);
        myCollider.offset = new Vector2(myCollider.offset.x, -(myColliderStartSize.y - (myCollider.size.y < myCollider.size.x? myCollider.size.x:myCollider.size.y))/2);
        mySprite.color = color;
    }
    public void Jump()
    {
        if (!grounded)
            return;
        myBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = false;
    }
}


// public void Roll(bool _rolling)
// {
//     rolling = _rolling;
//     if (rolling)
//         StartCoroutine(IRoll());
// }

// IEnumerator IRoll()
// {
//     while (rolling)
//     {
//         if (_SentientBeing.TestStaminaAction(1))
//             _SentientBeing.DoStaminaAction(1);
//         else
//             rolling = false;
//         yield return waitFrame;
//     }
// }