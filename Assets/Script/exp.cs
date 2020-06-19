using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exp : MonoBehaviour {

	public Vector3 pos;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
		pos = bullet.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		pos = bullet.transform.position;
		this.GetComponent<Rigidbody> ().AddExplosionForce (-100.0f,pos,100.0f, 3.0f);
	}
}
