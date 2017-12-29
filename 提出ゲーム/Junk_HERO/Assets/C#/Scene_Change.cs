using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//ステージ説明をするキャンバスのスクリプト
public class Scene_Change: MonoBehaviour {

	int stage_count;//ステージの数

	public int Clear_Stage;//クリアしたステージ

	public GameObject[] Selectable_Button;//今選択できるボタン

	GameObject[] AllButton;//すべてのボタン


	public static string SaveData_Key = "ClearData";

	public Canvas experience_canvas;//ステージを選択した後に出すキャンバス

	public Text des_text; // ステージの内容を説明するテキスト


	int stage_number;//選択したステージの番号

	public AudioSource AS;

	public AudioClip select_sound;//ステージを選択した時の音

	public AudioClip decide_sound;//ステージを決定した時の音

	public AudioClip cansel_sound;//キャンセルを押した時の音


	void Start () {

		//現在のステージ数を取得する。
		stage_count = PlayerPrefs.GetInt ("stage_count", 0);
		
		//ステージの説明用のキャンバスを非表示にする。
		experience_canvas.enabled = false;

		//クリアしたステージ数を表示する
		Clear_Stage = PlayerPrefs.GetInt (SaveData_Key, 0);

		//現在あるステージ分の配列を作成する。
		Selectable_Button = new GameObject[stage_count];

		//ゲームステージのボタンを探す処理
		for (int i = 0; i < stage_count; i++)
		
		{

			Selectable_Button[i] = 
				GameObject.Find ("/Select_Canvas/Button" + (i + 1))as GameObject;
			
		}

		//クリアした数に応じてステージを表示する処理
		for (int i = 0; i < stage_count; i++) 
		
		{
			
			if (i <= Clear_Stage) {
				
				Selectable_Button [i].SetActive (true);

			} else {
				
				Selectable_Button [i].SetActive (false);

			}
		
		}
	
	}
	

	void Update () {

	}


	//選んだステージの内容を表示する処理
	public void Description(int select_number){

		//説明用のキャンバスを表示する。
		experience_canvas.enabled = true;

		//ステージの番号ごとに表示内容を変更する
		switch (select_number){

		case 1:
			des_text.fontSize = 35;
			des_text.text = "チュートリアルステージです。\n" +
			"指示に従ってください。\n" +
			"サブ達成条件:ジャンクを100以上集める。";

			stage_number = 1;

			break;
		
		case 2:
			des_text.fontSize = 35;
			des_text.text = "Junkを集めて、先へ進んでください。\n" +
				"上の方にもジャンクがあるので、技1を活用してください。\n" +
				"サブ達成条件:ジャンクを100以上集める。";

			stage_number = 2;

			break;
		
		case 3:
			des_text.fontSize = 35;
			des_text.text = "ふさがった道を開いて" +
				"先に進んでください。\n" +
				"技を交換できる場所があります。\n" +
				"サブ達成条件:敵を全て倒す。";

			stage_number = 3;

			break;
		
		case 4:
			des_text.fontSize = 30;
			des_text.text = "巨大な装甲で武装した敵が現れました。\n" +
			"どうやら後ろの留め具を破壊すれば中を攻撃できるようです。\n" +
			"攻撃で怯んだ、との情報もあるので、それらを利用して敵を倒しましょう。\n" +
			"サブ達成条件:隠しアイテムを取得する";

			stage_number = 4;

			break;
		
		case 5:
			des_text.fontSize = 35;
			des_text.text = "フリーステージです。\n" +
			"無限のジャンクを用意してあり、自由に行動できます。";

			stage_number = 5;

			break;
		

		}
			
		AS.clip = select_sound;

		//選択した時の音を出す。
		AS.Play ();

	}


	//ステージに移動する処理
	public void Go_Stage(){
		
		StartCoroutine ("diray_go");

	}


	//キャンセルボタンを押した時の処理
	public void Cansel(){

		AS.clip = cansel_sound;

		//キャンセルした時の音を出す。
		AS.Play ();

		experience_canvas.enabled = false;

	}


	//選択シーンに移動する処理
	public void Select()
	{

		SceneManager.LoadScene ("Select_Scene");
	
	}


	//タイトルに戻る処理
	public void Title()
	{

		AS.clip = decide_sound;

		AS.Play ();

		SceneManager.LoadScene ("Title_Scene");
	
	}


	//シーンを移行する処理
	IEnumerator diray_go(){

		AS.clip = decide_sound;

		AS.Play ();

		yield return new WaitForSeconds (0.5f);

		switch (stage_number) {

		case 1:

			SceneManager.LoadScene ("Stage1_Scene");

			break;
		
		case 2:

			SceneManager.LoadScene ("Stage2_Scene");

			break;
		
		case 3:

			SceneManager.LoadScene ("Stage3_Scene");

			break;
		
		case 4:

			SceneManager.LoadScene ("StageF_Scene");

			break;
		
		case 5:

			SceneManager.LoadScene ("FreeStage_Scene");

			break;
		}

	}


}