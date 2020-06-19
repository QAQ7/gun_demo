using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShot : MonoBehaviour {
	
	public GameObject bullet;
	public float coldTime = 0.1f;
	private float shotTime;
	public GameObject controller;
	public float power = 100;
	public float range = 100;
	public GameObject shot_eff;
	// Use this for initialization

	void Start () {
		shotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Mouse0)){
			if(Time.time-shotTime>=coldTime){
				GameObject.Instantiate (shot_eff, this.transform.position, this.transform.rotation);
				GameObject newbullet = GameObject.Instantiate (bullet, this.transform.position,Quaternion.identity);
				newbullet.transform.rotation = this.transform.rotation;
				shotTime = Time.time;
			}
		}
		if (Input.GetKey (KeyCode.Mouse1)) {
			//Blocks.exps (controller.GetComponent<GameManager>().blocks,power,this.transform.position,range);
		}
	}
}
