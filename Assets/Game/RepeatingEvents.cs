using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingEvents : MonoBehaviour {

    static int frameCount = 0;
    static List<RepeatingMethod> methods = new List<RepeatingMethod>();

    public class RepeatingMethod
    {
        public MonoBehaviour script { get; set; }
        public string name { get; set; }
        public int frameInterval { get; set; }
        public int frameDelay { get; set; }
    }

    // Update is called once per frame
    void Update () 
    {
		for(int i = 0; i < methods.Count; i++)
        {            
            var m = methods[i];
            if (m.script == null)
                methods.Remove(m);
            else if (frameCount > m.frameDelay)
                if (frameCount % m.frameInterval == 0)
                    m.script.Invoke(m.name, 0);
        }
        frameCount++;
	}

    /// <summary>
    /// Requires the passed method to be public
    /// </summary>
    /// <param name="script"></param>
    /// <param name="name">String</param>
    /// <param name="frameInterval">Number of frames between executions</param>
    /// <param name="frameDelay">Number of frames to delay from the point of registering</param>
    public static void RegisterMethod(MonoBehaviour script, string name, int frameInterval, int frameDelay)
    {
        Debug.Log($"Registered new repeating event from {script.name}: {name}");
        methods.Add(new RepeatingMethod() {
            script = script,
            name = name,
            frameInterval = frameInterval,
            frameDelay = frameDelay
        });
    }
}
