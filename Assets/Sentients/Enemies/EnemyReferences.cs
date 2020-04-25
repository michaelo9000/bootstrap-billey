using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyReferences : SentientReferences 
{
    [HideInInspector]
	public EnemyControl _EnemyControl;
    [HideInInspector]
	public EnemyMove _EnemyMove;

	void Start () 
    {
        AssignInheritedReferences();
        _EnemyControl = GetComponent<EnemyControl>();
        _EnemyMove = GetComponent<EnemyMove>();
    }
}
