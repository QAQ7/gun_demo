using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

	public GameObject[] parts;
	public int curPartid = 0;

    // Start is called before the first frame update
    void Start()
    {
		parts [curPartid].SetActive (true);
    }

    // Update is called once per frame
    void Update()
    {
		if (parts [curPartid].GetComponent<Part> ().partEnd()) {
			changePart ();
		}
    }

	public void changePart(){
		if (parts [curPartid].GetComponent<Part> ().haveNextPart) {
			parts [curPartid].SetActive (false);
			parts [++curPartid].SetActive (true);
		}
	}
}
