using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Change: MonoBehaviour {//技を交換するオブジェクトの操作用のスクリプト


	bool abletoread = false; //交換できる位置かどうか

	public GameObject Change_Weapon; //交換する技のゲームオブジェクト

	public int changeNo;//変更する技番号

	GameObject Player;//プレイヤー

	public Canvas w_expe_canvas; //技の説明を行うキャンバス

	public Text changetext; //交換する技の説明を行うテキスト



	void Start () {

		//現在の技の能力を記入
		Text_Change();

		w_expe_canvas.enabled = false;
	}
	

	void Update () {

		if (abletoread && Input.GetKeyDown (KeyCode.X)) {
			
			if (w_expe_canvas.enabled == false) {
				
				w_expe_canvas.enabled = true;

			} else {
				
				w_expe_canvas.enabled = false;

			}
				
		}
			

		if (abletoread && Input.GetKeyDown (KeyCode.Return)) {

			GameObject exchange;

			switch (changeNo) {

			case 1:

				exchange = Player.GetComponent<Player_S_Select> ().S1;

				Player.GetComponent<Player_S_Select> ().S1 = Change_Weapon;

				Change_Weapon = exchange;

				if (changeNo == Player.GetComponent<Player_S_Select> ().Selected) {
					
					Player.GetComponent<Player_S_Select> ().S_select.text = 
						changeNo + ":" + Player.GetComponent<Player_S_Select> ().S1.name;
					
				}

				break;

			case 2:

				exchange = Player.GetComponent<Player_S_Select> ().S2;

				Player.GetComponent<Player_S_Select> ().S2 = Change_Weapon;

				Change_Weapon = exchange;

				if (changeNo == Player.GetComponent<Player_S_Select> ().Selected) {

					Player.GetComponent<Player_S_Select> ().S_select.text = 
						changeNo + ":" + Player.GetComponent<Player_S_Select> ().S2.name;

				}

				break;

			case 3:

				exchange = Player.GetComponent<Player_S_Select> ().S3;

				Player.GetComponent<Player_S_Select> ().S3 = Change_Weapon;

				Change_Weapon = exchange;

				if (changeNo == Player.GetComponent<Player_S_Select> ().Selected) {

					Player.GetComponent<Player_S_Select> ().S_select.text = 
						changeNo + ":" + Player.GetComponent<Player_S_Select> ().S3.name;

				}

				break;


			case 4:

				exchange = Player.GetComponent<Player_S_Select> ().S4;

				Player.GetComponent<Player_S_Select> ().S4 = Change_Weapon;

				Change_Weapon = exchange;

				if (changeNo == Player.GetComponent<Player_S_Select> ().Selected) {

					Player.GetComponent<Player_S_Select> ().S_select.text = 
						changeNo + ":" + Player.GetComponent<Player_S_Select> ().S4.name;

				}

				break;
				
			}

			Text_Change ();

		}

		
	}

	void OnTriggerStay(Collider col){

		if (col.tag == "Player") {
			
			abletoread = true;

			Player = col.gameObject;

		}
	}

	void OnTriggerExit(Collider col){

		if (col.tag == "Player") {
			abletoread = false;
		}
	}


	void Text_Change(){
		
		changetext.text = "Enterで交換\n 番号: S" + changeNo +
			"\n 交換する技 : " + Change_Weapon +
			"\n技の威力 : " + Change_Weapon.GetComponent<Wepon_States> ().ATK;
	}
}
