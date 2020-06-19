using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
	public GameObject gp;
	public float power;
	public float range;
	public GameObject point;
	public GameObject exp_ef;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Bullet") {
			GameObject.Instantiate (exp_ef, this.transform.position, this.transform.rotation);
			point.GetComponent<BlockRotate> ().speed = 0.0f;
			Blocks.movable (true, gp.GetComponent<Point> ().blocks);
			Blocks.exps (gp.GetComponent<Point> ().blocks, power, this.transform.position, range);
			this.gameObject.SetActive (false);
		}
	}
}
