using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerReferences : SentientReferences 
{
	public PlayerAttack _PlayerAttack;
	public PlayerControl _PlayerControls;
	public PlayerMove _PlayerMove;
    public Collider2D DuckCollider;
    public Collider2D DropCollider;
    public Collider2D RollCollider;
    public Sprite DuckSprite;
    public Sprite DropSprite;
    public Sprite RollSprite;

    void Start () 
    {
        AssignInheritedReferences();
        // If the component has not been set in the editor (i.e. it is null), find it on the gameObject.
        // This allows you to reference components that are not attached to the root gameObject.
        _PlayerAttack = _PlayerAttack ?? GetComponent<PlayerAttack>();
        _PlayerControls = _PlayerControls ?? GetComponent<PlayerControl>();
        _PlayerMove = _PlayerMove ?? GetComponent<PlayerMove>();
	}
}
