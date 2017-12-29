using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour { //技１のスクリプト
	
	Rigidbody rb; //Rigidbody

	public GameObject remain_object; //破壊された時に残るオブジェクト

	GameObject Player; //プレイヤー

	public AudioSource AS; //オーディオソース

	public AudioClip make_sound; //作られた時の音

	public AudioClip break_sound; //破壊された時の音


	void Start () {
		
		rb = GetComponent<Rigidbody> (); //Rigidbodyを取得

		Player = GameObject.Find ("Junk_Robot")as GameObject; //プレイヤーの取得

		rb.useGravity = false; //重力を解除する

		StartCoroutine ("CanMake"); //作成できるかどうか確認する処理

	}
	

	void Update () {

	}


	void OnCollisionEnter(Collision cln){

		//プレイヤーの技2、技3か、敵の攻撃を受けた時
		if (cln.collider.tag == "Pl_Atk"
		    || cln.collider.tag == "Break_Brock"
		    || cln.collider.tag == "En_Atk") {

			StartCoroutine ("broken"); //自分を破壊する処理を実行する

		}

	}


	void OnTriggerEnter (Collider col){
		
		{
			
			if (col.tag == "Break_Brock") { //プレイヤーの技３を受けた時
				
				StartCoroutine ("broken"); //自分を破壊する処理を実行する

			}

			//作られる前プレイヤー以外がオブジェクトに触れたとき
			if (col.tag != "Player" &&GetComponent<BoxCollider> ().isTrigger == true) {
				
				Player.GetComponent<Player_Attack> ().Total_Junks += 10; //使用したジャンクをプレイヤーに戻す

				Destroy (gameObject); //自分を破壊する

			}

		}

	}


	IEnumerator CanMake (){ //作れるときの処理
		
		yield return new WaitForSeconds (0.5f); //0.5秒待つ

		rb.useGravity = true; //重力を適用する

		GetComponent<BoxCollider> ().isTrigger = false; //BoxColliderのisTriggerを外す

		AS.clip = make_sound; //オーディオソースに作られた時の音を付ける

		AS.Play (); //オーディオソースを再生する

	}


	IEnumerator broken(){ //破壊されたときの処理
		
		GetComponent<BoxCollider> ().enabled = false; //オブジェクトの判定を外す

		GetComponent<MeshRenderer> ().enabled = false; //見えなくする

		AS.clip = break_sound; //オーディオソースに破壊された時の音を付ける

		AS.Play (); //オーディオソースを再生する

		Vector3 rp_transform = new Vector3 (

			transform.position.x,
			transform.position.y + 1f,
			transform.position.z

		); //インスタンティートするオブジェクトの位置を決定する


		GameObject remain_pop = 
			GameObject.Instantiate (remain_object,rp_transform,transform.rotation)
				as GameObject; //残るオブジェクトをインスタンティートする
		
		remain_pop.GetComponent<Release_Junk> ().Child_Color_Change
			(GetComponent<MeshRenderer>().material.color); //（残るオブジェクトを自分の色に変える処理を実行させる）処理を実行

		yield return new WaitForSeconds (0.5f); //0.5秒待つ


		Destroy (gameObject); // 自分を破壊する
	}


}