using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrogate : MonoBehaviour {

	GameObject Owner;

	public void SetOwner(GameObject owner)
	{
		Owner = owner;
	}

	// Update is called once per frame
	public void SetPosition () {
		transform.position = Owner.transform.position;
		transform.localScale = Owner.transform.localScale;
		transform.rotation = Owner.transform.rotation;
	}
}
