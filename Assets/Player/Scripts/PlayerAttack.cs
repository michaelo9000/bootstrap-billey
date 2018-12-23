using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    PlayerReferences Reference;
    public static bool windingUp;
    public static bool attacking;
    public GameObject attackBox;
    public float attackDelayFrames;
    public float attackFrames;
    public float attackStamina;
    
    private void Start()
    {
        Reference = GetComponent<PlayerReferences>();
    }
    public void DoAttack()
    {
        if(Reference._HealthAndDamage.TestStaminaAction(attackStamina))
            StartCoroutine(IAttack());
    }

    IEnumerator IAttack()
    {
        windingUp = true;
        int frameCount = 0;
        while (windingUp)
        {
            yield return Reference.FrameWait;
            if (frameCount > attackDelayFrames)
                windingUp = false;
            frameCount++;
        }
        Reference._HealthAndDamage.DoStaminaAction(attackStamina);
        attacking = true; 
        var atk = Instantiate(attackBox, transform);
        yield return new WaitForSeconds(attackFrames / 60);
        attacking = false;
        Destroy(atk);
    }
}
