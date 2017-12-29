using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//プレイヤのHPを管理するスクリプト
public class Player_HP : MonoBehaviour {

	int HP = 1;

	int stage_count;

	Text hp_text;

	void Start () {

		hp_text = GameObject.Find ("/States_Canvas/HP_Text").GetComponent<Text> ();

		stage_count = 	PlayerPrefs.GetInt("stage_count", 0);

		//サブ条件をクリアした分HPが増える処理
		for (int i = 0; i < stage_count; i++) {

			if (PlayerPrefs.GetInt ("Stage" + i, 0) == 1) {

				HP += 1;

			}

		}


		
	}
	

	void Update () {

		hp_text.text = "HP : " + HP;

		if (HP <= 0) {

			Death ();

		}
	}

	void OnCollisionEnter (Collision cln){

		//敵に触れた時ダメージを受ける処理
		if (cln.collider.tag == "Enemy"
			|| cln.collider.tag == "En_Atk") {

			HP -= 1;

		}

	}


	void Death(){

		Cursor.visible = true;

		Cursor.lockState = CursorLockMode.None;

		SceneManager.LoadScene ("GameOver_Scene");

	}


}