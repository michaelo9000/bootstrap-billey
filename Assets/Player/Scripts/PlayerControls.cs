using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    PlayerMove playerMove;
    PlayerAttack playerAttack;
    public int buttonTapFrames;
    public int buttonHoldFrames;
    WaitForEndOfFrame frameWait = new WaitForEndOfFrame();
    Coroutine EvadeCoroutine;
    public int evadeHeldFrames = 0;

    void Start () {
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
	}
	void Update () 
    {
        playerMove.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButton("Jump"))
            playerMove.Jump();

        if (Input.GetButtonDown("Evade"))
        {
            playerMove.Duck();
            EvadeCoroutine = StartCoroutine(CheckEvadeInput());
        }

        if (Input.GetButtonUp("Evade"))
        {
            StopCoroutine(EvadeCoroutine);
            if (evadeHeldFrames < buttonTapFrames)
                playerMove.DropEvade();
            else if (evadeHeldFrames < buttonHoldFrames)
                playerMove.RollEvade();
            else 
                playerMove.StandUp();
            evadeHeldFrames = 0;
        }

        if (Input.GetButtonDown("Attack"))
            playerAttack.DoAttack();
    }
    IEnumerator CheckEvadeInput(){
        while(true){
            yield return frameWait;
            evadeHeldFrames++;
        }
    }
}
