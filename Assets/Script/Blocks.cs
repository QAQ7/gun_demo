using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {

	public Material dis;
	public float disappearSpeed = 0.0001f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Bullet") {
			this.GetComponent<Blocks> ().disappear (this.gameObject);
			this.gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
		if (collision.gameObject.tag == "Plane") {
			this.GetComponent<Blocks> ().disappear (this.gameObject);
			GameObject.Destroy (this.gameObject);
		}
	}

	void exp(float power,Vector3 pos,float range){
		this.GetComponent<Rigidbody> ().AddExplosionForce (power,pos,range);
	}

	void disappear(GameObject block){
		this.GetComponent<Chunk> ().changeChunk ();
		//block.GetComponent<Renderer> ().material = dis;
		StartCoroutine(change(10.0f,Time.time,block));
	}

	IEnumerator change(float dietime,float createTime,GameObject block){
		while (Time.time - createTime < dietime) {
			block.GetComponent<Renderer> ().material.SetFloat ("_DissolvePercentage", block.GetComponent<Renderer> ().material.GetFloat ("_DissolvePercentage") + disappearSpeed);
			if (block.GetComponent<Renderer> ().material.GetFloat ("_DissolvePercentage") + disappearSpeed > 1) {
				block.GetComponent<Renderer> ().material.SetFloat ("_DissolvePercentage", 1.0f);
			}
			yield return 0;
		}
	}

	public static void movable(bool sign, GameObject[] blocks){
		if (sign == true) {
			for (int i = 0; i < blocks.Length; i++) {
				blocks [i].GetComponent<Rigidbody> ().constraints -= RigidbodyConstraints.FreezeAll;
				blocks [i].GetComponent<Rigidbody> ().useGravity = true;
			}	
		}
	}

	public static void exps(GameObject[] blocks,float power,Vector3 pos,float range){
		for (int i = 0; i < blocks.Length; i++) {
			blocks [i].GetComponent<Blocks> ().exp (power,pos,range);
			blocks [i].GetComponent<Blocks> ().disappear (blocks [i].gameObject);
		}
	}
}
