using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Destory : MonoBehaviour {
	public float lifeTime = 0.3f;
	private float starT;
	// Use this for initialization
	void Start () {
		starT = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - starT > lifeTime) {
			GameObject.Destroy (this.gameObject);
		}
	}
}
