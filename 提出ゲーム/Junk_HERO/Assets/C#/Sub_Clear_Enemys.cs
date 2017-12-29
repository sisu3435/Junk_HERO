using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Clear_Enemys : Sub_Clear {

	GameObject goal;

	public GameObject[] enemys;

	int enemy_count;

	// Use this for initialization
	void Start () {

		goal = GameObject.FindWithTag ("Goal");

		enemy_count = enemys.Length;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (enemy_count > 0) {

			for (int i = 0; i < enemys.Length; i++) {

				if (enemys [i] == null) {

					enemy_count -= 1;

					enemys [i] = gameObject;

				}

			}
		
		} else {

			subclear (goal);

			Destroy (gameObject);

		}

	}
}
