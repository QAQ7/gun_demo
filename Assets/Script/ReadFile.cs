using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class ReadFile : MonoBehaviour {

	public string path;

	// Use this for initialization
	void Start () {
		FileStream file = new FileStream(path, FileMode.Open);
		BinaryReader br = new BinaryReader(file);
		int line ;
		string[] pos;
		try{
			while (true)
			{
				line = br.ReadInt32();
				Debug.Log (line);
			}
		}catch(EndOfStreamException e){
			
		}
		file.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
