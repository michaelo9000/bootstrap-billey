using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SentientReferences : MonoBehaviour {

	public HealthAndDamage _HealthAndDamage;
    public CollisionKeeper _CollisionKeeper;
    public GlobalReferences _Global;
    public Rigidbody2D _Rigidbody2D;
    public Collider2D _Collider2D;
    public SpriteRenderer _SpriteRenderer;
    public Sprite _Sprite;
    public GameObject SurrogatePrefab;
    public Surrogate _Surrogate;
    public WaitForEndOfFrame FrameWait = new WaitForEndOfFrame();

	public void AssignInheritedReferences ()
    {
        // If the component has not been set in the editor (i.e. it is null), find it on the gameObject.
        // This allows you to reference components that are not attached to the root gameObject.
        _HealthAndDamage = _HealthAndDamage ?? GetComponent<HealthAndDamage>();
        _CollisionKeeper = _CollisionKeeper ?? GetComponent<CollisionKeeper>();

        _Global = _Global ?? GameObject.FindGameObjectWithTag("Master").GetComponent<GlobalReferences>();

        // These work differently for some reason
        if (_Rigidbody2D == null)
            _Rigidbody2D = GetComponent<Rigidbody2D>();
        if (_Collider2D == null)
            _Collider2D = GetComponent<CapsuleCollider2D>();
        if (_SpriteRenderer == null)
            _SpriteRenderer = GetComponent<SpriteRenderer>();

        _Sprite = _SpriteRenderer.sprite;

        var surrogate = Instantiate(SurrogatePrefab);
        surrogate.name = $"{gameObject.name} Surrogate";
        surrogate.transform.position = gameObject.transform.position;
        _Surrogate = surrogate.GetComponent<Surrogate>();
        _Surrogate.SetOwner(gameObject);
    }
}
