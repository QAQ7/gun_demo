using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block02 : MonoBehaviour
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
	public int id;
	public GameObject cam;
	public GameObject planeExp;
	public bool rised = false;
	public float fallSpeed = 1.0f;
	public Vector3 pos;

	// Use this for initialization

	void Start () {
		pos = new Vector3(this.transform.position.x,0,this.transform.position.z);
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		//this.gameObject.GetComponent<BlockFollow> ().enabled = false;
		StartCoroutine ("rise");
		mat = this.GetComponent<Renderer> ().material;
		this.gameObject.GetComponent<BlockFall> ().enabled = false;
		//Debug.Log (mat.color);
	}

	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<BlockFall> ().enabled == false) {
			this.transform.position = new Vector3 (pos.x + player.transform.position.x, this.transform.position.y, pos.z + player.transform.position.z);
		}
		if (!rised) {
			this.gameObject.GetComponent<BlockFall> ().enabled = false;
			Debug.Log ("rising");
		} else {
			this.gameObject.GetComponent<BlockFall> ().rised = true;
		}
		/*if (this.transform.position.y < -2) {
			missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			GameObject.DestroyImmediate (this.gameObject);
		}*/
		if (life <= 0) {
			//missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject,true);
			GameObject.Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Bullet") {
			mat.color = mat.color + new Color (0.15f, 0.15f, 0.15f);
			Debug.Log (mat.color);
			life--;
			if (life == 0) {
			//missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
				//missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject,true);
				GameObject.Instantiate (exp_ef, this.transform.position, this.transform.rotation);
				GameObject.Instantiate (money_ef, this.transform.position, this.transform.rotation);
			}
		}
		/*if (rised && collision.gameObject.tag == "Player") {
			life = 0;
			cam.GetComponent<ScreenShift> ().shift ();
			//missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject,true);
			//missionControler.GetComponent<Mission01_Part02> ().numOfems--;
			GameObject.Instantiate (exp_ef, this.transform.position, this.transform.rotation);
			GameObject.Instantiate (money_ef, this.transform.position, this.transform.rotation);
		}*/
		/*if (rised&&collision.gameObject.tag == "Plane") {
			life = 0;
			//missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			GameObject.Instantiate (planeExp, this.transform.position, planeExp.transform.rotation);
			this.gameObject.GetComponent<BlockFall> ().enabled = false;
			GameObject.DestroyImmediate (this.gameObject);
		}*/
		//Debug.Log (collision.collider.name);
	}

	public void fall(){
		StartCoroutine ("drop");
	}

	public void reCreate(){
		life = orglife;
		this.transform.position = new Vector3 (Random.Range(player.transform.position.x-20,player.transform.position.x+20),-1,Random.Range(player.transform.position.z-20,player.transform.position.z+20));
		StartCoroutine ("rise");
		//this.gameObject.GetComponent<BlockFollow> ().enabled = false;
	}

	IEnumerator change(float dietime,float createTime){
		while (Time.time - createTime < dietime) {
			this.transform.position += new Vector3(0,riseSpeed,0);
			yield return 0;
		}
	}

	IEnumerator rise(){
		yield return StartCoroutine(change(riseLong,Time.time));
		rised = true;
		//this.gameObject.GetComponent<BlockFollow> ().enabled = true;
	}

	IEnumerator close(){
		while (this.transform.position.y>-2) {
			this.transform.position -= new Vector3 (0, fallSpeed, 0);
		}
		yield return 0;
	}

	IEnumerator drop(){
		yield return StartCoroutine (close());
	}
}
