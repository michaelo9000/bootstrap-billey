using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Rigidbody2D myBody;
    SentientBeing _SentientBeing;
    public float moveSpeed;
    public int jumpForce;
    public int rollForce;
    public static bool grounded;
    bool rolling;

    private void Start()
    {
        _SentientBeing = gameObject.GetComponent<SentientBeing>();
    }

    public void Move(float h)
    {
        if(h != 0 && !rolling) //todo need to be able to change facing even when ducking
        {
            myBody.velocity = new Vector2(h * moveSpeed, myBody.velocity.y);
            //flip x axis
            if (h != 0 && Mathf.Sign(h) != transform.localScale.x)
                transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
        }
    }    
    public void Duck()
    {
        rolling = true;
        Debug.Log("duck");
    }
    public void DropEvade()
    {
        Debug.Log("drop");
    }
    public void RollEvade()
    {
        Debug.Log("roll");
        myBody.AddForce(Vector2.right * transform.localScale.x * rollForce, ForceMode2D.Impulse);
    }
    public void StandUp()
    {
        rolling = false;
        Debug.Log("stand");
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