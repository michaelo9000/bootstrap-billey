using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour {

    public float distanceAhead;
    public float distanceAbove;
    public float movementLerp;
    public Transform player;
    Rigidbody2D myBody;

    // Update is called once per frame
    void Start () {
        myBody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate () {
        //get the difference, not the position
        var newPos = new Vector2(
            //player.x position + modify to be ahead of the player
            (player.position.x + distanceAhead * player.localScale.x) - myBody.position.x,
            player.position.y + distanceAbove - myBody.position.y
        );

        if (myBody.position != newPos)
            myBody.MovePosition(myBody.position + newPos * Time.deltaTime * movementLerp);
	}
}
