using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makePlane : MonoBehaviour
{
	public GameObject plane;
	public GameObject player;
	public GameObject[,] planes = new GameObject[7,7];
	public int sizeX = 7;
	public int sizeY = 7;
	bool [,] planeNum = new bool[100,100];
	public int curX = 0;
	public int curY = 0;
    // Start is called before the first frame update
    void Start()
    {
		for (int i = -3; i < 4; i++) {
			for (int j = -3; j <4; j++) {
				planes[i+3,j+3] = GameObject.Instantiate (plane, new Vector3 (i * 10, -1, j * 10), Quaternion.identity);
			}
		}
		for (int i = 0; i < 100; i++) {
			for (int j = 0; j < 100; j++) {
				planeNum [i, j] = true;
			}
		}
		Debug.Log ("ok");
    }

    // Update is called once per frame
    void Update()
    {
		bool sign = false;
		if (player.transform.position.x > curX*10 + 10) {
			curX++;
			sign = true;
		}
		if(player.transform.position.x < curX*10 - 10){
			curX--;
			sign = true;
		}
		if (player.transform.position.z > curY*10 + 10) {
			curY++;
			sign = true;
		}
		if(player.transform.position.z < curY*10 - 10){
			curY--;
			sign = true;
		}
		if (sign == true) {
			for (int i = -3; i < 4; i++) {
				for (int j = -3; j <4; j++) {
					planes [i + 3, j + 3].transform.position = new Vector3 ((curX + i) * 10, -1, (curY + j) * 10);
				}
			}
		}
    }
}
