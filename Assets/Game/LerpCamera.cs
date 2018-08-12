using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour {

    public float aheadDistance;
    public float cameraLerp;
    public Transform player;

    // Update is called once per frame
    void Update () {
        var newPos = new Vector3(
            player.position.x + aheadDistance * player.localScale.x,
            player.position.y,
            transform.position.z);

        if (transform.position != newPos)
            transform.position = Vector3.Lerp(transform.position, newPos, 1 /cameraLerp);
	}
}
