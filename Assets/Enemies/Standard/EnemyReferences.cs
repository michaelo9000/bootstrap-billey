using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyReferences : SentientReferences 
{
	public EnemyAttack _EnemyAttack;
	public EnemyControl _EnemyControl;
	public EnemyMove _EnemyMove;

	void Start () 
    {
        AssignInheritedReferences();
        // If the component has not been set in the editor (i.e. it is null), find it on the gameObject.
        // This allows you to reference components that are not attached to the root gameObject.
        _EnemyAttack = _EnemyAttack ?? GetComponent<EnemyAttack>();
        _EnemyControl = _EnemyControl ?? GetComponent<EnemyControl>();
        _EnemyMove = _EnemyMove ?? GetComponent<EnemyMove>();
    }
}
