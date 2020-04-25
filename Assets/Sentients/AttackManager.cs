using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

    SentientReferences References;
    public bool WindingUp;
    public bool Attacking;
    
    private void Start()
    {
        References = GetComponent<SentientReferences>();
    }

    public void RaiseShield()
    {

    }

    public void DoAttack(int attackNumber = 0)
    {
        if (!Attacking)
        {
            // Select an attack
            attackNumber--;
            if (attackNumber < 0)
            {
                var attackCount = References.CurrentWeapon._Weapon.Attacks.Length - 1;
                attackNumber = Mathf.RoundToInt(Random.value * attackCount);
            }
            References.CurrentWeapon._Weapon.CurrentAttack = References.CurrentWeapon._Weapon.Attacks[attackNumber];

            if (References._HealthManager.TestStaminaAction(References.CurrentWeapon._Weapon.CurrentAttack.Stamina))
                StartCoroutine(IAttack());
        }
    }

    IEnumerator IAttack()
    {
        References.CanMove = false;
        var weapon = References.CurrentWeapon._Weapon;

        if (weapon.CurrentAttack.WindUpFrames > 0)
        {
            WindingUp = true;
            weapon.BeginWindUp();
            yield return Extensions.WaitForFrames(weapon.CurrentAttack.WindUpFrames);
            WindingUp = false;
        }

        weapon.BeginAttack();
        References._HealthManager.DoStaminaAction(weapon.CurrentAttack.Stamina);

        // If the attack does not have a set ActiveFrames, it is presumed to be active until the player ends it.
        if (weapon.CurrentAttack.ActiveFrames > 0)
        {
            Attacking = true; 
            yield return Extensions.WaitForFrames(weapon.CurrentAttack.ActiveFrames);
            weapon.EndAttack();
            Attacking = false;
        }

        References.CanMove = true;
    }
}
