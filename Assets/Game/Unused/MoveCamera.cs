using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

    List<Vector3> positions;
    public int framesBehind;
    public Transform player;
    public float cameraZ;
    public bool allowedToMove = true;
    Vector3 vel = Vector3.zero;

    private void Start()
    {
        positions = new List<Vector3>();
        createCameraTrackingPath(transform.position, player.position);
    }

    private void Update()
    {
        if (allowedToMove)
        {
            positions.Add(new Vector3(player.position.x, player.position.y, cameraZ));
            transform.position = Vector3.SmoothDamp(transform.position, positions[0], ref vel, .5f);
            positions.RemoveAt(0);
        }
    }

    public void disallowMovement()
    {
        allowedToMove = false;
        //createCameraTrackingPath(transform.position, movePlayer.startPos);
    }

    public void createCameraTrackingPath(Vector2 from, Vector2 to)
    {
        positions.Clear();
        var grad = new Vector2(to.x - from.x, to.y - from.y);
        for (int i = 0; i < framesBehind; i++)
        {
            positions.Add(new Vector3(from.x + grad.x * i / framesBehind, from.y + grad.y * i / framesBehind, cameraZ));
        }
    }
}