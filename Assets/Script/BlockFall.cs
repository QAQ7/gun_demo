using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFall : MonoBehaviour
{
	public float fallSpeed = 1.0f;
	public GameObject missionControler;
	public GameObject planeExp;
	public GameObject disappoint;
	public bool sign = false;
	public bool rised = false;

	public float speed = 1f;
	public GameObject target;

	Vector3 dir;
	Vector3 tarpos;
    // Start is called before the first frame update
    void Start()
    {
		this.gameObject.GetComponent<BoxCollider> ().size = new Vector3 (1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.position -= new Vector3 (0, fallSpeed, 0);
		dir = new Vector3(target.transform.position.x-this.transform.position.x,0,target.transform.position.z-this.transform.position.z);		
		dir.Normalize ();
		//dir.x*= Random.Range (0f, 10f);
		//dir.z*= Random.Range (0f, 10f);
		dir.y = 0;
		dir *= speed;
		this.transform.position += dir;
		if (this.transform.position.y < -5) {
			if(sign == false)
			missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			sign = true;
			GameObject.DestroyImmediate (this.gameObject);
		}
    }

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Plane") {
			//missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			GameObject.Instantiate (planeExp, this.transform.position, planeExp.transform.rotation);
			//this.gameObject.GetComponent<BlockFall> ().enabled = false;
			if(sign == false)
			missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			sign = true;
			GameObject.Destroy(this.gameObject);
		}
		if (rised && collision.gameObject.tag == "Player") {
			Debug.Log ("drop");
			GameObject.Instantiate (planeExp, this.transform.position, planeExp.transform.rotation);
			GameObject.Instantiate (disappoint, this.transform.position, disappoint.transform.rotation);
			if(sign == false)
			missionControler.GetComponent<Mission01_Part02> ().destroyGameobj (this.gameObject);
			sign = true;
			GameObject.Destroy(this.gameObject);
		}
	}
}
