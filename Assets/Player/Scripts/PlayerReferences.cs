using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerReferences : MonoBehaviour {

	public SentientBeing _SentientBeing;
	public PlayerAttack _PlayerAttack;
	public PlayerControls _PlayerControls;
	public PlayerMove _PlayerMove;
    public Rigidbody2D _Rigidbody2D;
    public CapsuleCollider2D _CapsuleCollider2D;
    public Vector2 ColliderStartSize;
    public SpriteRenderer _SpriteRenderer;
    public Vector2 SpriteStartSize;
    public WaitForEndOfFrame FrameWait = new WaitForEndOfFrame();
	void Start () {
        // If the component has not been set in the editor (i.e. it is null), find it on the gameObject.
        // This allows you to reference components that are not attached to the root gameObject.
        _PlayerAttack = _PlayerAttack ?? gameObject.GetComponent<PlayerAttack>();
        _PlayerControls = _PlayerControls ?? gameObject.GetComponent<PlayerControls>();
        _PlayerMove = _PlayerMove ?? gameObject.GetComponent<PlayerMove>();
        _SentientBeing = _SentientBeing ?? gameObject.GetComponent<SentientBeing>();
        _Rigidbody2D = _Rigidbody2D ?? gameObject.GetComponent<Rigidbody2D>();
        _CapsuleCollider2D = _CapsuleCollider2D ?? gameObject.GetComponent<CapsuleCollider2D>();
        _SpriteRenderer = _SpriteRenderer ?? gameObject.GetComponent<SpriteRenderer>();

        ColliderStartSize = _CapsuleCollider2D.size;
        SpriteStartSize = _SpriteRenderer.size;
	}
}
