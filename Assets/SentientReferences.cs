using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SentientReferences : MonoBehaviour {

	public HealthAndDamage _HealthAndDamage;
    public CollisionKeeper _CollisionKeeper;
    public Rigidbody2D _Rigidbody2D;
    public CapsuleCollider2D _CapsuleCollider2D;
    public Vector2 ColliderStartSize;
    public SpriteRenderer _SpriteRenderer;
    public Vector2 SpriteStartSize;
    public WaitForEndOfFrame FrameWait = new WaitForEndOfFrame();
	void Start () {
        // If the component has not been set in the editor (i.e. it is null), find it on the gameObject.
        // This allows you to reference components that are not attached to the root gameObject.
        _HealthAndDamage = _HealthAndDamage ?? GetComponent<HealthAndDamage>();
        _CollisionKeeper = _CollisionKeeper ?? GetComponent<CollisionKeeper>();
        // Doesn't work for these for some reason?
        _Rigidbody2D = _Rigidbody2D ?? GetComponent<Rigidbody2D>();
        _CapsuleCollider2D = _CapsuleCollider2D ?? GetComponent<CapsuleCollider2D>();
        _SpriteRenderer = _SpriteRenderer ?? GetComponent<SpriteRenderer>();

        ColliderStartSize = _CapsuleCollider2D.size;
        SpriteStartSize = _SpriteRenderer.size;
	}
}
