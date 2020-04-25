    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentientReferences : MonoBehaviour {

    [HideInInspector]
    public AttackManager _AttackManager;
    [HideInInspector]
	public HealthManager _HealthManager;
    [HideInInspector]
	public DamageManager _DamageManager;
    [HideInInspector]
    public CollisionKeeper _CollisionKeeper;

    [HideInInspector]
    public Rigidbody2D _Rigidbody2D;
    public Collider2D _Collider2D;

    [HideInInspector]
    public SpriteRenderer _SpriteRenderer;
    [HideInInspector]
    public Sprite _Sprite;

    [HideInInspector]
    public Surrogate _Surrogate;

    public HeldWeapon[] HeldWeapons;
    [SerializeField]
    public HeldWeapon CurrentWeapon;

    [System.Serializable]
    public class HeldWeapon
    {
        public GameObject WeaponPrefab;
        [HideInInspector]
        public GameObject WeaponObject;
        [HideInInspector]
        public Weapon _Weapon;
    }

    public bool CanMove = true;

	public void AssignInheritedReferences ()
    {
        _AttackManager = GetComponent<AttackManager>();
        _HealthManager = GetComponent<HealthManager>();

        // These don't have any public variables, so adding them at the runtime makes the prefab less cluttered
        _DamageManager = gameObject.AddComponent<DamageManager>();
        _CollisionKeeper = gameObject.AddComponent<CollisionKeeper>();

        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Collider2D = GetComponent<CapsuleCollider2D>();

        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Sprite = _SpriteRenderer.sprite;

        var surrogatePrefab = (GameObject)Resources.Load("Prefabs/Surrogate");
        var surrogate = Instantiate(surrogatePrefab);
        surrogate.layer = gameObject.layer;
        surrogate.name = $"{gameObject.name} Surrogate";
        surrogate.transform.position = gameObject.transform.position;
        _Surrogate = surrogate.GetComponent<Surrogate>();
        _Surrogate.SetOwner(gameObject);

        foreach (var weapon in HeldWeapons)
        {
            weapon.WeaponObject = Instantiate(weapon.WeaponPrefab, _Surrogate.gameObject.transform);
            weapon.WeaponObject.layer = gameObject.layer;
            weapon._Weapon = weapon.WeaponObject.GetComponent<Weapon>();
            weapon._Weapon.Owner = gameObject;
        }
    }
}
