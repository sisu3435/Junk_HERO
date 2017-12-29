using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Delete2 : MonoBehaviour { //一定以上の攻撃で破壊される処理のスクリプト

	public GameObject Gate; 

	public int Break_Atk; //このオブジェクトを壊すのに必要なATK(攻撃力)

	public Text B_Text;//Break_Atkを表示するテキスト

	public bool Break_flag = false; //破壊するフラグ


	void Start () {
	
		B_Text.text = "必要ATK : " + Break_Atk; //破壊に必要なATKを表示
	
	}


	void Update () {
		
		if (Break_flag) { //自分を破壊する処理

			Destroy (gameObject);

			Destroy (Gate);
		
		}
	
	}


	void OnCollisionEnter(Collision cln){

		if (cln.collider.tag == "Pl_Atk") { //プレイヤーからの攻撃を受けたとき

			//攻撃力の取得
			int ATK = cln.collider.GetComponent<Wepon_States> ().ATK;

			if (ATK >= Break_Atk) { //攻撃力が足りていれば壊れる処理
			
				Break_flag = true;
			
			} else { //ATKが足りていなければ必要攻撃力と現在の攻撃力を表示
				
				B_Text.text = "必要ATK : " + Break_Atk + "  ATK : " + ATK;
			
			}

		}
	
	}

}