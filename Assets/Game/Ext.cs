using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Ext {

    public static Vector2 As2D(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static Vector3 As3D(this Vector2 vector, float zed)
    {
        return new Vector3(vector.x, vector.y, zed);
    }
}
