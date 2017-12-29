using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//データを初期化する処理
public class Data_Inishialize : MonoBehaviour {

	//クリアしたステージ数を保存する変数を呼び出す名前
	string SaveData_Key;

	public int stage_count; //ステージの数

	//初期化する画面
	Canvas inishialize_canvas;

	void Start () {

		PlayerPrefs.SetInt ("stage_count", stage_count);

		SaveData_Key = Scene_Change.SaveData_Key;

		inishialize_canvas = GameObject.Find 
			("/Inishialize_Canvas").GetComponent<Canvas> ();

	}
	

	void Update () {

		
	}


	//初期化する画面を表示/非表示にする処理
	public void inishialize_view(){

		if (inishialize_canvas.enabled == false) {

			inishialize_canvas.enabled = true;

		} else {

			inishialize_canvas.enabled = false;

		}

	}


	//データを初期化する処理
	public void Inisialize(){

		PlayerPrefs.SetInt (SaveData_Key, 0);

		for (int i = 0; i < stage_count; i++) {

			PlayerPrefs.SetInt ("Stage" + i, 0);
		}

		inishialize_canvas.enabled = false;

	}


}
