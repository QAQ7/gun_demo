using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject blocks;
	public GameObject block1Controller;
	public GameObject block2Controller;
	public GameObject player;
	public const int GameTitle = 0;
	public const int GameRun = 1;
	public int CurMission = 1;
	public int State = 0;

	// Use this for initialization
	void Start () {
		//block1Controller.SetActive (false);
		//block2Controller.SetActive (false);
		//setAllDisactive (player);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

/*	void MissionLoop(int CurMission, int State){
		switch(CurMission){
		case(1):{
				Mission1 (State);
				break;
			}
		}
	}
*/
/*	void Mission1(int State){
		switch(CurMission){
		case(0):{

				break;
			}
		case(1):{
				if (Mission1StateChange (1)) {
					return;
				}
				break;
			}
		}
	}
*/
/*	public bool Mission1StateChange(int CurState){
		switch(CurState){
		case(0):{
				State++;
				block1Controller.SetActive (true);
				return true;
			}
		case(1):{
				//if (blocks.Length == 0) {
					State++;
					return true;
				//}
				return false;
			}
		}
	}
*/
	void setAllDisactive(GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			obj [i].SetActive (false);
		}
	}

	void setAllActive(GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			obj [i].SetActive (true);
		}
	}
}
