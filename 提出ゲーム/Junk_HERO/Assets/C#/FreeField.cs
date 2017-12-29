using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//フリーフィールドのみ発動する処理をかきこむスクリプト
public class FreeField : MonoBehaviour {

	public GameObject player; //プレイヤー


	void Start () {
		
	}
	

	void Update () {
		//プレイヤーの所持junk数を999に固定する
		player.GetComponent<Player_Attack> ().Total_Junks = 999;

	}

}