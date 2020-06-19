using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{

	public GameObject nextPart;
	public bool haveNextPart;

	public GameObject[] ems;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public static void setAllActive(GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			obj [i].SetActive (true);
		}
	}

	public virtual bool partEnd(){
		return false;
	}
}
