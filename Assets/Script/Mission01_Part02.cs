using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission01_Part02 : Part
{
	public int numOfems;
	public int numExist;
	public float coldTime = 5.0f;
	Queue<int> list = new Queue<int>();
	bool[] idList = new bool[7 * 7];

	void Start(){
		numOfems = ems.Length;
		int num = Random.Range (0,numOfems-1);
		list.Enqueue (num);
		idList [0] = true;
		for(int i = 1;i < numOfems;i ++){
			num = Random.Range (0,numOfems);
			while(list.Contains(num)){
				num = Random.Range (0,numOfems);
			}
			Debug.Log (num);
			list.Enqueue (num);
			idList [i] = true;
		}
		Part.setAllActive (ems);
		Invoke ("letFall", coldTime);
	}

	public void destroyGameobj(GameObject obj){
		for (int i = 0; i < ems.Length; i++) {
			if (ems [i].Equals (obj)) {
				idList [i] = false;
				//GameObject.Destroy (ems [i]);
				break;
			}
		}
		numOfems--;
		if (numOfems > 0) {
			while (!idList [list.Peek ()]) {
				Debug.Log (list.Peek());
				list.Dequeue ();
			}
			ems [list.Peek ()].GetComponent<BlockFall> ().enabled = true;
			list.Dequeue ();
		}
	}

	public void destroyGameobj(GameObject obj,bool s){
		for (int i = 0; i < ems.Length; i++) {
			if (ems [i].Equals (obj)) {
				idList [i] = false;
				//GameObject.Destroy (ems [i]);
				break;
			}
		}
		numOfems--;
	}

	public override bool partEnd(){
		if (numOfems <= 0) {
			return true;
		}
		return false;
	}

	IEnumerator change(float dietime,float createTime){
		while (Time.time - createTime < dietime) {
			yield return 0;
		}
	}

	IEnumerator timer(){
		yield return StartCoroutine (change (coldTime, Time.time));
		//this.gameObject.GetComponent<BlockFollow> ().enabled = true;
	}

	void letFall(){
		Debug.Log ("falling");
		ems[list.Peek()].GetComponent<BlockFall> ().enabled = true;
		list.Dequeue ();
	}
		
	/*
	public int numOfems;
	public int numExist;
	public GameObject planeControler;
	GameObject[,] planes;
	public GameObject block;
	public GameObject[] myems = new GameObject[7 * 7];
	public float coldTime = 2.0f;
	public GameObject player;
	bool risend = false;
	Queue<int> list = new Queue<int>();
	bool[] idList = new bool[7 * 7];

	// Start is called before the first frame update
	void Start()
	{
		planes = planeControler.GetComponent<makePlane> ().planes;
		int x = planeControler.GetComponent<makePlane> ().sizeX;
		int y = planeControler.GetComponent<makePlane> ().sizeY;
		numOfems = x*y;
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				//planes [i, j].GetComponent<PlaneControl> ().enabled = true;
				myems [y * i + j] = GameObject.Instantiate (block, planes [i , j].transform.position, planes [i , j].transform.rotation);
				myems [y * i + j].GetComponent<Block02> ().id = y * i + j;
				myems [y * i + j].GetComponent<Block02> ().player = player;
				myems [y * i + j].GetComponent<Block02> ().missionControler = this.gameObject;
				idList [y * i + j] = true;
				//myems [y * i + j].GetComponent<Block02> ().fallSpeed = 1.0f;
			}
		}
		int num = Random.Range (-1,numOfems);
		list.Enqueue (num);
		for(int i = 0;i < numOfems;i ++){
			while(list.Contains(num)){
				num = Random.Range (-1,numOfems);
			}
			list.Enqueue (num);
		}
		//StartCoroutine ("timer");
		Debug.Log (list.Peek());
		//myems[list.Peek()].GetComponent<BlockFall> ().enabled = true;
		Debug.Log (myems[list.Peek()].GetComponent<BlockFall> ().enabled);
		list.Dequeue ();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void destroyGameobj(GameObject obj){
		int id = 0;
		numOfems--;
		idList [id] = false;
		if (numOfems > 0) {
			while (!idList [list.Peek ()]) {
				Debug.Log (list.Peek());
				list.Dequeue ();
			}
			myems [list.Peek ()].GetComponent<BlockFall> ().enabled = true;
			list.Dequeue ();
		}
	}

	public override bool partEnd(){
		if (numOfems <= 0) {
			return true;
		}
		return false;
	}

	IEnumerator change(float dietime,float createTime){
		while (Time.time - createTime < dietime) {
			yield return 0;
		}
	}

	IEnumerator timer(){
		yield return StartCoroutine (change (coldTime, Time.time));
		//this.gameObject.GetComponent<BlockFollow> ().enabled = true;
	}*/
}
