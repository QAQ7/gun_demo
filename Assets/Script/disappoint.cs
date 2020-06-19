using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappoint : MonoBehaviour
{

	public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.LookAt (player.transform);
		this.transform.Rotate (90,90,90);
    }
}
