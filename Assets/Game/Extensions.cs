using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 FlipX(this Vector3 vector3)
    {
        var newVector3 = new Vector3
        (
            vector3.x * -1,
            vector3.y,
            vector3.z
        );
        return newVector3;
    }

    public static IEnumerator WaitForFrames(int framesToWait)
    {
        var frameCount = 0;
        while (frameCount <= framesToWait)
        {
            yield return StaticGlobalReferences.FrameWait;
            frameCount++;
        }
    }
}
