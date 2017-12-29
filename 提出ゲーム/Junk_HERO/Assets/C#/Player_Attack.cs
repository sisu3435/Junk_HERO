// 9/13 作成

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーの攻撃を管理するスクリプト
public class Player_Attack : MonoBehaviour {

	public bool NowDiray = false; //待ち中か

	GameObject bullet_pop;// 攻撃オブジェクト

	public Transform shot;//攻撃オブジェクトを出す位置

	public Transform pop;//オブジェクトを出す位置

	public bool Handed = false; //武器を持っているか

	Text Junk_Count;//junkの数を示すテキスト

	public int Total_Junks = 0; // 現在持っているジャンクの総数

	GameObject S1; //技１

	GameObject S2; //技2

	GameObject S3; //技3

	GameObject S4; //技4

	AudioSource AS; //オーディオソース

	public AudioClip BreakArea_pop;//break_areaを出す音

	public AudioClip make_sound; //lanceを作成した時の音

	public AudioClip Lance_Shot;//lanceを撃ち込む音


	void Start () {

		Junk_Count = GameObject.Find ("/States_Canvas/Junk_Count").GetComponent<Text> ();
		

		AS = GetComponent<Player_Move> ().AS;
		
	}
	

	void Update () {

		Junk_Count.text = "junk : " + Total_Junks;

		float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f); //向きの取得

		//何も持っていないか持った状態でマウスの左ボタンを離した時、物を手放すフラグを立てる
		if ((Handed && Input.GetMouseButton (0))
		    && bullet_pop != null) {

			bullet_pop.transform.position = new Vector3 (
				shot.transform.position.x + Mathf.Sin (angleDir) * 1.5f,
				shot.transform.position.y,
				shot.transform.position.z + Mathf.Cos (angleDir) * 1.5f);

		} else {

			Handed = false;
		}

		if (bullet_pop != null &&
			!Handed && bullet_pop.GetComponent<Ak_Con>() != null){//手元に持った武器を投げる

			bullet_pop.transform.SetParent (null); //親子関係を解除

			bullet_pop.transform.position = new Vector3( //位置を取り直す
				shot.transform.position.x + Mathf.Sin (angleDir) * 1.5f,
				shot.transform.position.y,
				shot.transform.position.z + Mathf.Cos (angleDir) * 1.5f);

			angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f); //向きの取得

			Vector3 dir = new Vector3 (Mathf.Sin (angleDir), 0, Mathf.Cos (angleDir)); //前後移動用

			bullet_pop.GetComponent<Ak_Con> ().Shoot (dir); //自分の向いている方向に飛ばす処理を実行

			bullet_pop = null; //自分の持つものをなくす

		}

		//技の発動をする処理
		if (!NowDiray && Input.GetMouseButtonDown (0)) {

			int Selected = GetComponent<Player_S_Select> ().Selected;

			Special_Select (Selected);

			NowDiray = true;

		}
		
	}


	//技を選択し、発動する処理
	public void Special_Select(int Selected){

		//発動後、技を発動できなくなる時間を入れる変数
		float diray = 0; 

		//現在プレイヤーが持っているjunkの総数
		int Total_Junks = GetComponent<Player_Attack> ().Total_Junks; 

		//必要なジャンク数
		int use_junk; 

		//どの技番号か判断
		switch (Selected) {

		//技１を発動
		case 1:

			//現在の技１を取得
			S1 = GetComponent<Player_S_Select> ().S1; 

			//
			use_junk = S1.GetComponent<Wepon_States> ().Use_junk;

			//必要なジャンクの数を持っているなら作成する
			if (Total_Junks >= use_junk) {

				Total_Junks -= use_junk;

				float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);

				Vector3 bullet_position = new Vector3 (
					transform.position.x + Mathf.Sin (angleDir) * 3,
					transform.position.y + 2f,
					transform.position.z + Mathf.Cos (angleDir) * 3);

				//インスタンティートする
				bullet_pop = GameObject.Instantiate (S1,bullet_position,transform.rotation)as GameObject;


				diray = 0.5f;

			}

			break;

		case 2: //技2を発動
			
			S2 = GetComponent<Player_S_Select> ().S2;

			use_junk = S2.GetComponent<Wepon_States> ().Use_junk;

			if (Total_Junks >= use_junk) {

				Total_Junks -= use_junk;

				float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f); //向きの取得

				Vector3 bullet_position = new Vector3 (
					shot.transform.position.x + Mathf.Sin (angleDir) * 1.5f,
					shot.transform.position.y,
					shot.transform.position.z + Mathf.Cos (angleDir) * 1.5f);
					
				bullet_pop = GameObject.Instantiate (S2,bullet_position,transform.rotation)as GameObject;

				bullet_pop.transform.SetParent (shot,true);

				AS.clip = make_sound; //オーディオソースに作られた時の音を付ける

				AS.Play (); //オーディオソースを再生する

				Handed = true;

				diray = 1f;

			}

			break;

		case 3: //技3を発動

			S3 = GetComponent<Player_S_Select> ().S3;

			use_junk = S3.GetComponent<Wepon_States> ().Use_junk;

			if (Total_Junks >= use_junk) {

				bullet_pop = GameObject.Instantiate 
					(S3, transform.position, transform.rotation);

				diray = 0.5f;

			}

			break;



		case 4: //技4を発動
			
			S4 = GetComponent<Player_S_Select> ().S4;

			use_junk = S4.GetComponent<Wepon_States> ().Use_junk;

			if (Total_Junks >= use_junk) {

				bullet_pop = GameObject.Instantiate (S4, pop.position, transform.rotation)as GameObject;

				diray = 2f;

			}

			break;


		}


		GetComponent<Player_Attack> ().Total_Junks = Total_Junks;

		//連続で技を発動できないようにする処理
		StartCoroutine ("Dlay", diray);

	}


	//攻撃をした後、攻撃できなくなる時間の処理
	private IEnumerator Dlay(float diray)
	{
		yield return new WaitForSeconds (diray);

		NowDiray = false;



	}


}
