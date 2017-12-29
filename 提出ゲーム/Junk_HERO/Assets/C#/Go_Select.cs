using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトルのキャンバスを管理するスクリプト
public class Go_Select: MonoBehaviour {

	//オーディオソース
	AudioSource AS;

	//ボタンをクリックした時の音
	public AudioClip decide_sound;

	void Start () {

		AS = GetComponent<AudioSource> ();
		
	}
	

	void Update () {
		
	}


	//選択シーンに移動する処理
	public void Select()
	{

		StartCoroutine ("go_select");
	
	}


	IEnumerator go_select(){


		AS.clip = decide_sound;

		//ボタンを押した時の音を出す。
		AS.Play ();

		yield return new WaitForSeconds (0.5f);

		//選択画面に移行する。
		SceneManager.LoadScene ("Select_Scene");

	}


}