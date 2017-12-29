using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ゴールについた時のスクリプト
public class Stage_Clear : MonoBehaviour { 

	//ステージの番号
	public int stage_number;

	//サブ条件が達成されたら
	public bool sub_clear = false;

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

			//サブ条件を達成していたらそれを保存する。
			if (sub_clear) {

				PlayerPrefs.SetInt ("Stage" + stage_number, 1); 

			}

			//カーソルを見えるようにする
			Cursor.visible = true; 

			//カーソルを動かせるようにする
			Cursor.lockState = CursorLockMode.None;

			//クリアシーンに移動
			SceneManager.LoadScene ("Clear_Scene");

		}
	
	}


}