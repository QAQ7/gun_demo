using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public const int orglife = 10;
	public int life = 10;
	public GameObject exp_ef;
	public Material mat;
	public GameObject money_ef;
	public float riseLong = 1;
	public float riseSpeed = 0.1f;
	public GameObject player;
	public GameObject missionControler;
	public GameObject cam;
	public GameObject disappoint;
	// Use this for initialization

	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		this.gameObject.GetComponent<BlockFollow> ().enabled = false;
		StartCoroutine ("rise");
		mat = this.GetComponent<Renderer> ().material;
		Debug.Log (mat.color);
	}
		
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			GameObject.Instantiate (exp_ef, this.transform.position, this.transform.rotation);
			GameObject.Instantiate (money_ef, this.transform.position, this.transform.rotation);
			missionControler.GetComponent<Mission01_Part01>().destroyGameobj (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Bullet") {
			mat.color = mat.color + new Color (0.15f, 0.15f, 0.15f);
			Debug.Log (mat.color);
			life--;
		}
		if (collision.gameObject.tag == "Player") {
			life = 0;
			GameObject.Instantiate (disappoint, this.transform.position, disappoint.transform.rotation);
			cam.GetComponent<ScreenShift> ().shift ();
		}
		Debug.Log (collision.collider.name);
	}

	public void reCreate(){
		life = orglife;
		this.transform.position = new Vector3 (Random.Range(player.transform.position.x-20,player.transform.position.x+20),-1,Random.Range(player.transform.position.z-20,player.transform.position.z+20));
		StartCoroutine ("rise");
		this.gameObject.GetComponent<BlockFollow> ().enabled = false;
	}

	IEnumerator change(float dietime,float createTime){
		while (Time.time - createTime < dietime) {
			this.transform.position += new Vector3(0,riseSpeed,0);
			yield return 0;
		}
	}

	IEnumerator rise(){
		yield return StartCoroutine(change(riseLong,Time.time));
		this.gameObject.GetComponent<BlockFollow> ().enabled = true;
	}
}
