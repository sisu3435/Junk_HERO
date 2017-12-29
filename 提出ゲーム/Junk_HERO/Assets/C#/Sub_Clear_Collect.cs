using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Clear_Collect :Sub_Clear {

	//ゴールの取得
	GameObject goal;

	//プレイヤーの取得
	GameObject player;

	//集める数
	public int collect_count;

	void Start () {

		//ゴールの取得
		goal = GameObject.FindWithTag ("Goal");

		//プレイヤーの取得
		player = GameObject.Find ("Junk_Robot")as GameObject; 

	}
	

	void Update () {

		if (player.GetComponent<Player_Attack> ().Total_Junks > collect_count) {

			subclear (goal);

			Destroy (gameObject);

		}

	}


}