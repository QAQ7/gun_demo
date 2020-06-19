using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	public GameObject bullet;
	public float speed = 0.1f;
	public float dieTime = 2.0f;
	private float createTime;
	public GameObject exp_eff;
	// Use this for initialization
	void Start () {
		createTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		bullet.transform.Rotate (0, 0, 2);
		bullet.transform.Translate (new Vector3 (0, 0, speed));
		if (Time.time - createTime >= dieTime) {
			GameObject.Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag=="Blocks"||collision.gameObject.tag=="Plane"){
			GameObject.Instantiate (exp_eff, this.transform.position, this.transform.rotation);
			GameObject.Destroy (this.gameObject);
		}
	}
}
