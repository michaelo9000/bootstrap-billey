using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    PlayerReferences References;
    bool Dead;

    public int buttonTapFrames;
    public int buttonHoldFrames;
    Coroutine EvadeCoroutine;
    public int evadeHeldFrames = 0;
    public SpriteRenderer rollIndicator;

    bool RightTriggerHeldLastFrame = false;
    bool LeftTriggerHeldLastFrame = false;
    Coroutine ShieldCoroutine;
    public int shieldHeldFrames = 0;

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
            if (true)//References._PlayerMove.stature != PlayerMove.Stature.rolling)
            {
                if (References._PlayerMove.stature == PlayerMove.Stature.standing)
                    References._PlayerMove.DuckEvade();
                EvadeCoroutine = StartCoroutine(CheckEvadeInput());
            }
        }

        if (Input.GetButton("Evade"))
        {
            if (evadeHeldFrames > buttonTapFrames && evadeHeldFrames < buttonHoldFrames)
                rollIndicator.color = Color.cyan;
            else 
                rollIndicator.color = Color.white;
        }

        if (Input.GetButtonUp("Evade"))
        {
            StopCoroutine(EvadeCoroutine);
            if (evadeHeldFrames >= 0 && evadeHeldFrames < buttonTapFrames)
            {
                if (References._PlayerMove.stature == PlayerMove.Stature.dropped)
                    References._PlayerMove.StandUp();
                else
                    References._PlayerMove.DropEvade();
            }
            else if (evadeHeldFrames >= buttonTapFrames && evadeHeldFrames < buttonHoldFrames)
            {
                References._PlayerMove.RollEvade();
            }
            else
            {
                References._PlayerMove.StandUp();
            }
            evadeHeldFrames = 0;
            rollIndicator.color = Color.white;
        }

        if (Input.GetButtonDown("Attack1"))
        {
            References.CurrentWeapon = References.HeldWeapons[0];
            References._AttackManager.DoAttack(1);
        }

        if (Input.GetAxis("RightTrigger") > 0.5 && !RightTriggerHeldLastFrame)
        {
            References.CurrentWeapon = References.HeldWeapons[0];
            References._AttackManager.DoAttack(2);
        }

        // LeftTrigger is being held
        if (Input.GetAxis("LeftTrigger") != 0)
        {
            References.CurrentWeapon = References.HeldWeapons[1];
            // LeftTrigger has only just begun being held
            if (!LeftTriggerHeldLastFrame)
            {
                ShieldCoroutine = StartCoroutine(CheckShieldInput());
                References._AttackManager.DoAttack(1);
            }
        }
        // LeftTrigger was released
        if (Input.GetAxis("LeftTrigger") == 0 && LeftTriggerHeldLastFrame)
        {
            StopCoroutine(ShieldCoroutine);
            if (shieldHeldFrames >= 0 && shieldHeldFrames < buttonTapFrames)
            {
                References._AttackManager.DoAttack(2);
            }
            else if (shieldHeldFrames >= buttonTapFrames && shieldHeldFrames < buttonHoldFrames)
            {
                // Some kind of shield charge?
                // And then lower shield
                References.CurrentWeapon._Weapon.EndAttack();
            }
            else
            {
                References.CurrentWeapon._Weapon.EndAttack();
            }
            shieldHeldFrames = 0;
        }

        RightTriggerHeldLastFrame = Input.GetAxis("RightTrigger") > 0.5;
        LeftTriggerHeldLastFrame = Input.GetAxis("LeftTrigger") != 0;

        References._Surrogate.SetPosition();
    }

    IEnumerator CheckEvadeInput()
    {
        while (true)
        {
            yield return StaticGlobalReferences.FrameWait;
            evadeHeldFrames++;
        }
    }

    IEnumerator CheckShieldInput()
    {
        while (true)
        {
            yield return StaticGlobalReferences.FrameWait;
            shieldHeldFrames++;
        }
    }

    public IEnumerator HandleDeath()
    {
        if (Dead)
            yield break;

        Dead = true;
        References._SpriteRenderer.color = Color.black;
        yield return new WaitForSeconds(1);
        StaticGlobalReferences._GameLord.Respawn();
        Destroy(gameObject);
    }
}
