using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission01_Part03 : Part
{
	public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
		for(int i=0;i<10;i++){
			for(int j=0;j<10;j++){
				for(int z=0;z<10;z++){
						GameObject objs = GameObject.Instantiate (obj,this.transform);
						objs.transform.position += new Vector3 (i,j,z);
					}
				}
			}
		}

    // Update is called once per frame
    void Update()
    {
        
    }
}
