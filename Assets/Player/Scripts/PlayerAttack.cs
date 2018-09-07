using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    public static bool windingUp;
    public static bool attacking;
    public GameObject attackBox;
    public float attackDelayFrames;
    public float attackFrames;
    public float attackStamina;
    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
    
    public void DoAttack()
    {
        if(gameObject.GetComponent<SentientBeing>().TestStaminaAction(attackStamina))
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
        gameObject.GetComponent<SentientBeing>().DoStaminaAction(attackStamina);
        attacking = true; 
        var atk = Instantiate(attackBox, transform);
        yield return new WaitForSeconds(attackFrames / 60);
        attacking = false;
        Destroy(atk);
    }
}
