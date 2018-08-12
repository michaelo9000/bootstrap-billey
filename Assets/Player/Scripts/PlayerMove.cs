using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Rigidbody2D myBody;
    public CapsuleCollider2D myCollider;
    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

    public float moveSpeed;
    public float moveLerp;
    Vector2 startPos;

    public Vector2 jumpPower;
    public static bool grounded;

    public static bool rolling;
    public int rollFrames;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Move(float h)
    {
        var pos = transform.position;
        transform.position = Vector2.Lerp(pos, new Vector2(pos.x + h * moveSpeed * (rolling ? 2 : 1), pos.y), 1 / moveLerp);
        if (h != 0 && Mathf.Sign(h) != transform.localScale.x)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
    }

    public void Jump()
    {
        if (!grounded)
            return;
        myBody.velocity = Vector2.zero;
        myBody.AddForce(jumpPower, ForceMode2D.Impulse);
    }

    public void HitGround()
    {
        grounded = true;
    }

    public void LeftGround()
    {
        grounded = false;
    }

    public void Roll(bool _rolling)
    {
        rolling = _rolling;
        if (rolling)
            StartCoroutine(IRoll());
    }

    IEnumerator IRoll()
    {
        myCollider.enabled = false;
        int frameCount = 0;
        while (rolling)
        {
            yield return waitFrame;
            if (frameCount > rollFrames)
                rolling = false;
            frameCount++;
        }
        myCollider.enabled = true;
    }
}
