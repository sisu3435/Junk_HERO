using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Junk_Break : MonoBehaviour { //一定数のジャンクを回収して破壊されるオブジェクト

	public int required_j_number; //破壊されるまでに必要なジャンク数

	int recovery_number = 0; //現在回収されたジャンク数

	public Text rjn_text; //必要なジャンク数を表すテキスト

	GameObject Player;



	void Start () {
		
	}
	

	void Update () {

		rjn_text.text = "必要なジャンク数 = " + required_j_number +
			" \n 現在回収したジャンク数 = " + recovery_number;

		if (recovery_number >= required_j_number) { //必要分集まったら破壊する

			Destroy (gameObject);

		}

		if (Player != null //このオブジェクトに入れたjunkを取り出す
			&& Input.GetKeyDown (KeyCode.Return)) {

			//10以上あるなら10取り出す
			if (recovery_number >= 10) {

				Player.GetComponent<Player_Attack> ().Total_Junks += 10;
				recovery_number -= 10;

			} else {
				
				Player.GetComponent<Player_Attack> ().Total_Junks += recovery_number;
				recovery_number = 0;

			}


		}


	}


	void OnTriggerEnter(Collider col){ //範囲内のjunkを回収する

		if (col.tag == "Junk") {
			Destroy (col.gameObject);
			recovery_number += 1;
		}

	}


	void OnTriggerStay (Collider col){

		if (col.tag == "Player") {
			
			Player = col.gameObject;

		}

	}

	void OnTriggerExit (Collider col){

		if (col.tag == "Player") {

			Player = null;

		}

	}

}