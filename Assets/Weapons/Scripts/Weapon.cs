using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class Weapon : MonoBehaviour
{
    public GameObject Owner;
    SentientReferences OwnerReferences;
    CollisionKeeper _CollisionKeeper;

    public Collider2D _Collider;

    public Vector2 InitialPosition;
    public float InitialRotation;

    public Attack[] Attacks;
    [HideInInspector]
    public Attack CurrentAttack;

    [System.Serializable]
    public class Attack
    {
        public string Name;
        // Each attack has a charged version and a non-charged, tied to the same button (held or tapped).
        // The shield's parry is the charged version of raising it.
        public int ChargeUpFrames;
        public Vector2 ActivePosition;
        public float ActiveRotation;
        public Vector2 WindUpPosition;
        public float WindUpRotation;
        public int WindUpFrames;
        public int ActiveFrames;
        public float Damage;
        public float Stamina;
    }

    void Start()
    {
        OwnerReferences = Owner.GetComponent<SentientReferences>();
        _CollisionKeeper = GetComponent<CollisionKeeper>();
        _Collider = gameObject.GetComponent<Collider2D>();
        InitialPosition = gameObject.transform.localPosition;
        InitialRotation = gameObject.transform.localEulerAngles.z;
    }

    public void BeginWindUp()
    {
        _Collider.enabled = true;
        gameObject.transform.localPosition = CurrentAttack.WindUpPosition;
        gameObject.transform.localEulerAngles = new Vector3(0, 0, CurrentAttack.WindUpRotation);
    }

    public void BeginAttack()
    {
        _Collider.enabled = true;
        gameObject.transform.localPosition = CurrentAttack.ActivePosition;
        gameObject.transform.localEulerAngles = new Vector3(0, 0, CurrentAttack.ActiveRotation);
    }

    public virtual void EndAttack()
    {
        gameObject.transform.localPosition = InitialPosition;
        gameObject.transform.localEulerAngles = new Vector3(0, 0, InitialRotation);
        _Collider.enabled = false;
        OwnerReferences._AttackManager.Attacking = false;
    }

    public void DamageCollision(GameObject collision)
    {
        // Don't damage yourself, fool
        if (collision.GetInstanceID() == Owner.GetInstanceID())
            return;

        OwnerReferences._DamageManager.DealDamage(CurrentAttack.Damage, collision);
    }
}
