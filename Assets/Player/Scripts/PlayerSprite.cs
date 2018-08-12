using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {

    //TODO replace with animation
    public SpriteRenderer mySprite;
    public Sprite walk;
    public Sprite startAttack;
    public Sprite attack;
    public Sprite roll;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerAttack.windingUp)
            mySprite.sprite = startAttack;
        else if (PlayerAttack.attacking)
            mySprite.sprite = attack;
        else if (!PlayerMove.grounded)
            mySprite.sprite = walk;
        else if (PlayerMove.rolling)
            mySprite.sprite = roll;
        else
            mySprite.sprite = walk;
    }
}
