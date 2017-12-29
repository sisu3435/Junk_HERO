using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junks_Con : MonoBehaviour { //Junksの処理のスクリプト
	

	public bool Deleted = false; // 消去するか否か

	public GameObject Player; // プレイヤーの位置を取得する

	Rigidbody rb; //リジッドボディ

	Vector3 Garbage_dump; // ステージの外に落ちた時に移動する場所

	bool last_lighting; // Junk_Lightingのlightingを最後に参照した時の値。

	public Material nomal_material; //通常時のマテリアル

	public Material change_material; //変化時のマテリアル

	public AudioClip pick_sound; //取得時の音

	Vector4 color;

	void Start () {
		
		rb = GetComponent<Rigidbody> (); //リジッドボディの取得

		//プレイヤーの取得
		Player = GameObject.Find ("Junk_Robot")as GameObject; 

		//ステージ外にでた時に戻るステージの位置を取得
		Garbage_dump = GameObject.Find ("Garbage_dump").transform.position;

	}
	

	void Update () {

		//最後に見たlightingと今のlightingが違っていたらマテリアルを切り替える処理に移行する
		if (last_lighting != Player.GetComponent<Junks_Lighting> ().Lighting) {
		
			Change_Material (Player.GetComponent<Junks_Lighting> ().Lighting);
		
		}

		//地面より下にいってしまった場合、ステージ内に戻す
		if (transform.position.y < -3) { 
			transform.position = Garbage_dump;
			rb.velocity = new Vector3 (0, 0, 0);
		}

		if (Deleted) { //消去する
			Destroy (gameObject);
		}


		float distance = Vector3.Distance 
			(Player.transform.position, transform.position); //プレイヤーとの距離

		float UD = Mathf.Abs (transform.position.y - Player.transform.position.y);//プレイヤーとのy方向の距離

		// 高低差が0.5以内、かつ距離が10以内ならプレイヤーの方へ移動する処理
		if (distance < 10 && UD < 0.5f) { 
			transform.LookAt (Player.transform.position); //プレイヤーの方を向く

			float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f); 
			Vector3 dir = new Vector3 (
				Mathf.Sin (angleDir), 
				0,
				Mathf.Cos (angleDir)); //向きの取得

			rb.velocity = dir * 10; //プレイヤーの方へ近よせる。
		}

	}


	void OnTriggerEnter(Collider col){
		
		//プレイヤーのisTriggerに触れた時、プレイヤーのジャンク保持数を増やして自分を破壊する
		if (col.tag == "Player"  && this.tag == "Junk") { 
			
			col.GetComponent<Player_Attack> ().Total_Junks += 1; 
			col.GetComponent<AudioSource>().clip = pick_sound;
			col.GetComponent<AudioSource> ().Play ();
			Deleted = true; //消去する

		}

	}

	//マテリアルを変える処理
	void Change_Material (bool lighting){

		//lightingがtrueなら光るマテリアルに変える、falseなら元のマテリアルに戻る
		if (lighting) {
			GetComponent<MeshRenderer> ().material = change_material;
		} else {
			GetComponent<MeshRenderer> ().material = nomal_material;
			Change_Color (color);
		}

		last_lighting = lighting;

	}

	public void Change_Color(Vector4 clr){

		GetComponent<MeshRenderer> ().material.color = clr;

		color = clr;

	}

}