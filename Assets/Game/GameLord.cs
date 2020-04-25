using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLord : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject HUDPrefab;
    public GameObject CameraPrefab;
    public LerpCamera Camera;

    public bool panCameraToStart;
    public float cameraStartDelay;
    public float playerStartDelay;

    // Use this for initialization
    IEnumerator Start ()
    {
        StaticGlobalReferences._GameLord = this;
        var HUD = Instantiate(HUDPrefab);
        var player = Instantiate(PlayerPrefab);
        StaticGlobalReferences._Player = player;
        var playerMoveScript = player.GetComponent<PlayerControl>();

        Camera = Instantiate(CameraPrefab).GetComponent<LerpCamera>();

        if (panCameraToStart)
        {
            Camera.enabled = false;
            playerMoveScript.enabled = false;
            yield return new WaitForSeconds(cameraStartDelay);
            Camera.enabled = true;
            yield return new WaitForSeconds(playerStartDelay);
            playerMoveScript.enabled = true;
        }
    }

    public void Respawn()
    {
        StartCoroutine(IRespawn());
    }

    IEnumerator IRespawn()
    {
        yield return new WaitForSeconds(playerStartDelay);
        StaticGlobalReferences._Player = Instantiate(PlayerPrefab);
    }
}
