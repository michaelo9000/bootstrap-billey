using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : SentientReferences 
{
    [HideInInspector]
	public PlayerControl _PlayerControls;
    [HideInInspector]
	public PlayerMove _PlayerMove;
    
    [HideInInspector]
    public Vector2 RespawnPosition;

    public Collider2D DuckCollider;
    public Collider2D DropCollider;
    public Collider2D RollCollider;
    public Sprite DuckSprite;
    public Sprite DropSprite;
    public Sprite RollSprite;

    void Start () 
    {
        AssignInheritedReferences();
        RespawnPosition = gameObject.transform.position;
        _PlayerControls = GetComponent<PlayerControl>();
        _PlayerMove = GetComponent<PlayerMove>();
	}
}
