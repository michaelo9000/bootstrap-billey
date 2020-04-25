using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    public void ActuallyEndAttack()
    {
        gameObject.transform.localPosition = InitialPosition;
        gameObject.transform.localEulerAngles = new Vector3(0, 0, InitialRotation);
        _Collider.enabled = false;
    }
}
