using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    public static bool windingUp;
    public static bool attacking;
    public GameObject attackBox;
    public float attackDelayFrames;
    public float attackFrames;
    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
    
    public void DoAttack()
    {
        StartCoroutine(IAttack());
    }

    IEnumerator IAttack()
    {
        windingUp = true;
        int frameCount = 0;
        while (windingUp)
        {
            yield return waitFrame;
            if (frameCount > attackDelayFrames)
                windingUp = false;
            frameCount++;
        }
        attacking = true; 
        var atk = Instantiate(attackBox, transform);
        yield return new WaitForSeconds(attackFrames / 60);
        attacking = false;
        Destroy(atk);
    }
}
