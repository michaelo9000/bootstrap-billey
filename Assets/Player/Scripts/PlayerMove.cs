using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Rigidbody2D myBody;
    SentientBeing _SentientBeing;
    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
    public float moveSpeed;
    Vector2 startPos;
    public Vector2 jumpPower;
    public static bool grounded;
    public static bool rolling;

    private void Start()
    {
        startPos = transform.position;
        _SentientBeing = gameObject.GetComponent<SentientBeing>();
    }

    public void Move(float h)
    {
        if(h != 0)
        {
            myBody.velocity = new Vector2(h * moveSpeed, myBody.velocity.y);
            //flip x axis
            if (h != 0 && Mathf.Sign(h) != transform.localScale.x)
                transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
        }
    }

    public void Jump()
    {
        if (!grounded)
            return;
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
        while (rolling)
        {
            if (_SentientBeing.TestStaminaAction(1))
                _SentientBeing.DoStaminaAction(1);
            else
                rolling = false;
            yield return waitFrame;
        }
    }
}
