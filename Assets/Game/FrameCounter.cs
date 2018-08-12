using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCounter : MonoBehaviour {

    public Text counter;
    int totalFramesOneSecondAgo;

    private IEnumerator Start()
    {
        while (1 == 1)
        {
            totalFramesOneSecondAgo = Time.frameCount;
            yield return new WaitForSeconds(1);
            counter.text = (Time.frameCount - totalFramesOneSecondAgo).ToString();
        }
    }
}
