using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    PlayerReferences Reference;
    public int buttonTapFrames;
    public int buttonHoldFrames;
    Coroutine EvadeCoroutine;
    public int evadeHeldFrames = 0;
    public SpriteRenderer rollIndicator;

    void Start () {
        Reference = GetComponent<PlayerReferences>();
	}
	void Update () 
    {
        Reference._PlayerMove.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
            Reference._PlayerMove.Jump();

        if (Input.GetButtonDown("Evade"))
        {
            if (Reference._PlayerMove.stature == PlayerMove.Stature.standing)
                Reference._PlayerMove.DuckEvade();
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
                if (Reference._PlayerMove.stature == PlayerMove.Stature.dropped)
                    Reference._PlayerMove.StandUp();
                else
                    Reference._PlayerMove.DropEvade();
            }
            else if (evadeHeldFrames < buttonHoldFrames)
            {
                Reference._PlayerMove.RollEvade();
            }
            else 
                Reference._PlayerMove.StandUp();
            evadeHeldFrames = 0;
            rollIndicator.color = Color.white;
        }

        if (Input.GetButtonDown("Attack"))
            Reference._PlayerAttack.DoAttack();
    }
    // TODO make generic, for other button hold checking
    /// <summary>
    /// Starts on button down, counts the number of frames continuously and stops on button up. 
    /// Used to get how long the button was held for.
    /// </summary>
    IEnumerator CheckEvadeInput()
    {
        while (true)
        {
            yield return Reference.FrameWait;
            evadeHeldFrames++;
        }
    }
}
