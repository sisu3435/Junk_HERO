using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゴールについた時のスクリプト
public class NextStage : MonoBehaviour {

	public int stage_number;

	void Start () {
		
	}
	

	void Update () {
		
	}


	void OnTriggerEnter(Collider col){

		//プレイヤーが触れたらクリアしたステージを管理する数値を変更し、クリアシーンに移行する
		if (col.tag == "Player") {

			//クリアしたステージの最も大きい番号
			int clear_number = PlayerPrefs.GetInt (Scene_Change.SaveData_Key, 0);

			//今回のステージ番号が最も大きかったら
			if (clear_number < stage_number) {
				
				PlayerPrefs.SetInt (Scene_Change.SaveData_Key, stage_number);

			}

			Cursor.visible = true; // カーソルを見えるようにする

			Cursor.lockState = CursorLockMode.None;// カーソルを動かせるようにする

			SceneManager.LoadScene ("Clear_Scene");

		}
	
	}


}