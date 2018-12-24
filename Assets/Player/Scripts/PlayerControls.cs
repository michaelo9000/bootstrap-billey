using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    PlayerReferences References;
    public int buttonTapFrames;
    public int buttonHoldFrames;
    Coroutine EvadeCoroutine;
    public int evadeHeldFrames = 0;
    public SpriteRenderer rollIndicator;

    void Start () 
    {
        References = GetComponent<PlayerReferences>();
	}
	void Update () 
    {
        References._PlayerMove.Move(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump"))
            References._PlayerMove.Jump();

        if (Input.GetButtonDown("Evade"))
        {
            if (References._PlayerMove.stature == PlayerMove.Stature.standing)
                References._PlayerMove.DuckEvade();
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
                if (References._PlayerMove.stature == PlayerMove.Stature.dropped)
                    References._PlayerMove.StandUp();
                else
                    References._PlayerMove.DropEvade();
            }
            else if (evadeHeldFrames < buttonHoldFrames)
            {
                References._PlayerMove.RollEvade();
            }
            else 
                References._PlayerMove.StandUp();
            evadeHeldFrames = 0;
            rollIndicator.color = Color.white;
        }

        if (Input.GetButtonDown("Attack"))
            References._PlayerAttack.DoAttack();
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
            yield return References.FrameWait;
            evadeHeldFrames++;
        }
    }
}
