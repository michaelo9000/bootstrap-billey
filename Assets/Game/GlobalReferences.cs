using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour {

	public Transform _Player;

	// Use this for initialization
	void Start () 
	{
        _Player = _Player ?? GameObject.FindGameObjectWithTag("Player").transform;
	}
}
