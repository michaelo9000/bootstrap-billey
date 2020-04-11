using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    
    EnemyReferences References;
    public static bool windingUp;
    public static bool attacking;
    public GameObject attackBox;
    public float attackDelayFrames;
    public float attackFrames;
    public float attackStamina;
    
    private void Start()
    {
        References = GetComponent<EnemyReferences>();
    }
    public void DoAttack()
    {
        if (References._HealthAndDamage.TestStaminaAction(attackStamina))
            StartCoroutine(IAttack());
    }

    IEnumerator IAttack()
    {
        windingUp = true;
        int frameCount = 0;
        while (windingUp)
        {
            yield return References.FrameWait;
            if (frameCount > attackDelayFrames)
                windingUp = false;
            frameCount++;
        }
        References._HealthAndDamage.DoStaminaAction(attackStamina);
        attacking = true; 
        var attackObject = Instantiate(attackBox, References._Surrogate.transform);
        attackObject.GetComponent<DoesDamage>().Owner = gameObject;
        yield return new WaitForSeconds(attackFrames / 60);
        attacking = false;
        Destroy(attackObject);
    }
}
