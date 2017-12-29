using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//playerのキャンバスを管理するスクリプト
public class PC_Canvas_Controll : MonoBehaviour {

	GameObject player;


	public Text Basic_Text; //基本操作の説明のタイトルのテキスト

	public Text Basic_experience; //基本説明のテキスト


	public Text Technique_Text; //技の説明のタイトルのテキスト

	public Text S1_Text; //技１に付いているテキスト

	public Text S2_Text; //技2に付いているテキスト

	public Text S3_Text; //技3に付いているテキスト

	public Text S4_Text; //技4に付いているテキスト

	Canvas PC_Canvas; // 操作説明用のキャンバス

	Canvas States_Canvas; // 普段見えているキャンバス


	void Start () {

		player = GameObject.Find ("Junk_Robot")as GameObject;

		Technique_Experience_Change ();

		States_Canvas = GameObject.Find ("/States_Canvas").GetComponent<Canvas> ();

		PC_Canvas = GameObject.Find ("/Player_Controll_Canvas").GetComponent<Canvas> ();

		Menu_OnOff (true, false);
		
	}
	

	void Update () {

		if (Input.GetKeyUp (KeyCode.Q)) {

			if (player.GetComponent<Player_Move> ().enabled == true) {

				Menu_OnOff (false, true);

			} else {

				Menu_OnOff (true, false);

			}

		}


		//見せるページの切り替え
		if (Input.GetKeyDown (KeyCode.D) 
			|| Input.GetKeyDown(KeyCode.A)) {

			
			if (Basic_Text.enabled == true) {
				
				enabled_change (false, true);

			} else if (Technique_Text.enabled == true) {
				
				enabled_change (true, false);

			}

		}


		//セレクトシーンに戻る処理
		if (this.GetComponentInParent<Canvas> ().enabled == true && Input.GetKey (KeyCode.Backspace)) {
			
			Cursor.visible = true;

			Cursor.lockState = CursorLockMode.None;

			SceneManager.LoadScene ("Select_Scene");

		}
		
	}

	//メニュー画面を開く、もしくは閉じる処理
	void Menu_OnOff(bool states_canvas,bool pc_canvas){
		
		player.GetComponent<Player_Move> ().enabled = states_canvas;

		States_Canvas.enabled = states_canvas;

		PC_Canvas.enabled = pc_canvas;
		
	}


	//PC_Canvasの中の表示画面の切り替え
	void enabled_change ( bool b_on_off , bool t_on_off){


		Basic_Text.enabled = b_on_off;

		Basic_experience.enabled = b_on_off;

		Technique_Text.enabled = t_on_off;

		S1_Text.enabled = t_on_off;

		S2_Text.enabled = t_on_off;

		S3_Text.enabled = t_on_off;

		S4_Text.enabled = t_on_off;

	}


	//技の説明を書き込む
	public void Technique_Experience_Change(){ 

		S1_Text.text = player.GetComponent<Player_S_Select> ().S1.GetComponent<Wepon_Experience> ().ex_txt.text;

		S2_Text.text = player.GetComponent<Player_S_Select> ().S2.GetComponent<Wepon_Experience> ().ex_txt.text;

		S3_Text.text = player.GetComponent<Player_S_Select> ().S3.GetComponent<Wepon_Experience> ().ex_txt.text;

		S4_Text.text = player.GetComponent<Player_S_Select> ().S4.GetComponent<Wepon_Experience> ().ex_txt.text;

	}


}