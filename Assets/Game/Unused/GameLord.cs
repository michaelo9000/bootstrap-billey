using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLord : MonoBehaviour {

    public bool panCameraToStart;
    public LerpCamera cameraScript;
    public PlayerMove playerMoveScript;
    public float cameraStartDelay;
    public float playerStartDelay;

    // Use this for initialization
    IEnumerator Start ()
    {
        if (panCameraToStart)
        {
            cameraScript.enabled = false;
            playerMoveScript.enabled = false;
            yield return new WaitForSeconds(cameraStartDelay);
            cameraScript.enabled = true;
            yield return new WaitForSeconds(playerStartDelay);
            playerMoveScript.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
