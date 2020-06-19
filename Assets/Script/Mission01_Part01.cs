using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission01_Part01 : Part
{
	public int numOfems = 20;
	public int numExist;

    // Start is called before the first frame update
    void Start()
    {
		setAllActive (ems);
		numExist = ems.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void destroyGameobj(GameObject obj){
		for (int i = 0; i < ems.Length; i++) {
			if (ems [i].Equals (obj)) {
				if (numOfems >= numExist) {
					obj.GetComponent<Block> ().reCreate ();
				} else {
					GameObject.Destroy (ems [i]);
				}
				numOfems--;
			}
		}
	}
		
	public override bool partEnd(){
		if (numOfems <= 0) {
			return true;
		}
		return false;
	}
}
