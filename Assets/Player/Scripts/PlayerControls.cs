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
    public SpriteRenderer rollIndicator;

    void Start () {
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
	}
	void Update () 
    {
        playerMove.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
            playerMove.Jump();

        if (Input.GetButtonDown("Evade"))
        {
            if (playerMove.stature == PlayerMove.Stature.standing)
                playerMove.DuckEvade();
            EvadeCoroutine = StartCoroutine(CheckEvadeInput());
        }

        if(Input.GetButton("Evade"))
        {
            if(evadeHeldFrames > buttonTapFrames && evadeHeldFrames < buttonHoldFrames)
                rollIndicator.color = Color.cyan;
            else 
                rollIndicator.color = Color.white;
        }

        if (Input.GetButtonUp("Evade"))
        {
            StopCoroutine(EvadeCoroutine);
            if (evadeHeldFrames < buttonTapFrames)
            {
                if (playerMove.stature == PlayerMove.Stature.dropped)
                    playerMove.StandUp();
                else
                    playerMove.DropEvade();
            }
            else if (evadeHeldFrames < buttonHoldFrames)
            {
                playerMove.RollEvade();
            }
            else 
                playerMove.StandUp();
            evadeHeldFrames = 0;
            rollIndicator.color = Color.white;
        }

        if (Input.GetButtonDown("Attack"))
            playerAttack.DoAttack();
    }
    // TODO make generic, for other button hold checking
    /// <summary>
    /// Starts on button down, counts the number of frames continuously and stops on button up. 
    /// Used to get how long the button was held for.
    /// </summary>
    IEnumerator CheckEvadeInput(){
        while(true){
            yield return frameWait;
            evadeHeldFrames++;
        }
    }
}
