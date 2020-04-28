using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	public float time = 10.0f;
	private void Start()
	{
		Destroy(gameObject, time);
	}
}
