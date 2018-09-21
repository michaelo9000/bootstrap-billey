using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour {

    public float aheadDistance;
    public float movementLerp;
    public Transform player;
    Rigidbody2D myBody;

    // Update is called once per frame
    void Start () {
        myBody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate () {    
        var newPos = new Vector2(
            //player.x position + modify to be ahead of the player
            (player.position.x + aheadDistance * player.localScale.x) - myBody.position.x,
            player.position.y - myBody.position.y
        );

        if (myBody.position != newPos)
            myBody.MovePosition(myBody.position + newPos * Time.deltaTime * movementLerp);
	}
}
