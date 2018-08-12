using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    PlayerMove playerMove;
    PlayerAttack playerAttack;

    void Start () {
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
	}
	
	void Update () {

        playerMove.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButton("Jump"))
            playerMove.Jump();

        if (Input.GetButtonDown("Roll"))
            playerMove.Roll(true);

        if (Input.GetButtonUp("Roll"))
            playerMove.Roll(false);

        if (Input.GetButtonDown("Attack"))
            playerAttack.DoAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerMove.HitGround();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerMove.LeftGround();
    }
}
