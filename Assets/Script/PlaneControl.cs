using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
	public GameObject block;
	public GameObject blockCreated;
	public float riseLong = 4;
	public float riseSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
	{
		//blockCreate ();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public GameObject blockCreate(){
		blockCreated = GameObject.Instantiate (block, this.transform.position, this.transform.rotation);
		blockCreated.GetComponent<BlockFollow>().enabled = false;
		//StartCoroutine ("rise");
		return blockCreated;
	}

	IEnumerator change(float dietime,float createTime){
		while (Time.time - createTime < dietime) {
			blockCreated.transform.position += new Vector3(0,riseSpeed,0);
			yield return 0;
		}
	}

	IEnumerator rise(){
		yield return StartCoroutine(change(riseLong,Time.time));
		blockCreated.GetComponent<BlockFollow> ().enabled = true;
	}
}
