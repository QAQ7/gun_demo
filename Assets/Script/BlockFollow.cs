using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFollow : MonoBehaviour {

	public float speed = 1f;
	public GameObject target;
	public float basey = 1;
	public float freezeTimeMin = 0.25f;
	public float freezeTimeMax = 0.5f;
	public float waitTimeMin = 0.25f;
	public float waitTimeMax = 0.5f;

	float freezeTime = 0.0f;
	bool sign = true;
	float curTime = 0.0f;
	float waitTime = 0.0f;
	Vector3 dir;
	Vector3 tarpos;
	// Use this for initialization
	void Start () {
		Make ();
		curTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (freezeTime < Time.time - curTime) {
			if (sign) {
				curTime = Time.time;
				waitTime = Random.Range (waitTimeMin, waitTimeMax);
				sign = false;
			}
			if (Time.time - curTime < waitTime) {
				return;
			}
			sign = true;
			Make ();
			curTime = Time.time;
			freezeTime = Random.Range (freezeTimeMin, freezeTimeMax);
		} 
		this.transform.position += dir;
		}

	void Make(){
		dir = new Vector3(target.transform.position.x-this.transform.position.x,0,target.transform.position.z-this.transform.position.z);		
		dir.Normalize ();
		//dir.x*= Random.Range (0f, 10f);
		//dir.z*= Random.Range (0f, 10f);
		dir.y = 0;
		dir *= speed;
		tarpos = new Vector3 (this.transform.position.x + dir.x, basey, this.transform.position.z + dir.z);
	}
}