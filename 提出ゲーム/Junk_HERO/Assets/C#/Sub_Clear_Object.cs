using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//オブジェクトに触れた時サブ条件を達成したことを送るスクリプト
public class Sub_Clear_Object : Sub_Clear {

	GameObject goal;


	void Start () {

		goal = GameObject.FindWithTag ("Goal");
		
	}
	

	void Update () {
		
	}

	void OnCollisionEnter(Collision cln){

		if (cln.collider.tag == "Player") {

			subclear (goal);

			Destroy (gameObject);

		}

	}
}
