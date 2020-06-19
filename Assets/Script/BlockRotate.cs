using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotate : MonoBehaviour {

	public float speed = 5;
	public GameObject gameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (speed, speed, speed);
		//this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
	}
}
