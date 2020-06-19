using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateChunk : MonoBehaviour {

	public GameObject[] blocks;
	public GameObject[,,] blocksinorder;
	public int x,y,z;
	Vector3Int range;
	public int[,,] id;

	// Use this for initialization
	void Start () {
		range = new Vector3Int (x,y,z);
		id = new int[range.x,range.y,range.z];
		blocksinorder = new GameObject[range.x,range.y,range.z];
		for (int i = 0; i < range.x; i++) {
			for (int j = 0; j < range.y; j++) {
				for (int k = 0; k < range.z; k++) {
					id [i, j, k] = 0;
					blocksinorder [i, j, k] = null;
				}
			}
		}
		for (int i = 0; i < blocks.Length; i++) {
			blocks [i].GetComponent<Chunk> ().getpos ();
			id [blocks [i].GetComponent<Chunk> ().x, blocks [i].GetComponent<Chunk> ().y, blocks [i].GetComponent<Chunk> ().z] = 1;
			blocksinorder [blocks [i].GetComponent<Chunk> ().x, blocks [i].GetComponent<Chunk> ().y, blocks [i].GetComponent<Chunk> ().z] = blocks [i];
		}
		for (int i = 0; i < blocks.Length; i++) {
			blocks [i].GetComponent<Chunk> ().makeChunk ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
