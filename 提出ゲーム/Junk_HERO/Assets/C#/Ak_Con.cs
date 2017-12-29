using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの攻撃用オブジェクトの処理をするスクリプト
public class Ak_Con : MonoBehaviour { 
	
	Rigidbody rb; //リジッドボディ

	public float break_count;//自壊するまでの秒数

	public GameObject Two_Junks;//20個のjunk

	public GameObject Ten_Junks;//10個のjunk

	public GameObject junk; //1個のjunk

	public bool CountStart = false; //破壊されるまでのカウントを始めるかを示すフラグ

	bool Breaked = false;//すでにBrokenが反応していることを示す

	public AudioClip Hit_sound;//敵に当たったときの音

	public AudioSource AS;//オーディオソース

	public float throw_power; //投げる力

	public GameObject hit_position; //ヒットするポジションを表示するオブジェクト

	public LayerMask mask;

	GameObject target;

	void Start () {
		
		rb = GetComponent<Rigidbody> (); //リジッドボディの取得

		target = Instantiate (hit_position, 
			transform.position, transform.rotation);

		target.SetActive (false);
	}


	void Update () {


		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (target != null) {
			
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {

				target.transform.position = hit.point;

				target.transform.rotation = transform.rotation;

				target.SetActive (true);

			} else {

				target.transform.position = transform.position;

				target.SetActive (false);

			}

		}

		
		if (CountStart) { //オブジェクトを破壊するカウントを開始する
			
			break_count -= Time.deltaTime;//破壊されるまでの時間

		} 

		//破壊までの秒数が０以下になった時破壊するフラグを建てる
		if (break_count <= 0) {

			Breaked = true; //破壊処理に移行するフラグを建てる

			break_count = 1; //この処理を何度も呼ばないためにbreak_countに代入

			
		}

		if (Breaked) {//破壊処理に移行する
			
			CountStart = false; //破壊されるカウントを止める

			//残すジャンクを落とす位置を決定する
			Vector3 Now_position = transform.position;

			//破壊処理
			StartCoroutine ("Broken");

			//破壊処理を何度も実行しないようにフラグを折る
			Breaked = false;

		}
	}

	//コリジョンに触れた時の処理
	void OnCollisionEnter (Collision cln) {
		
		if (cln.collider.tag != "Player" && !Breaked) {//プレイヤー以外にぶつかった時壊れる	
			Breaked = true;

		}

	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Boss" &&
			!Breaked) {// Bossの攻撃を受けた時壊れる

			Breaked = true;

		}

	}


	IEnumerator Broken(){ //自分を破壊する処理

		Destroy (target);

		GameObject junk_pop;//インスタンティートするオブジェクト

		//破壊された後残るjunkの数を取得
		int Use_Junk_Count = GetComponent<Wepon_States> ().Use_junk;
		
		GetComponent<MeshRenderer> ().enabled = false; //meshを見えなくする

		GetComponent<BoxCollider> ().enabled = false; //colliderを外す

		int Twenty = Use_Junk_Count / 20; //20junksをいくつ出すかを計算

		int Ten = (Use_Junk_Count % 20) / 10; //10junksをいくつ出すかを計算

		int one = Use_Junk_Count - (Twenty * 20 + Ten + 10);

		for (int i = 0; i < Twenty; i++) { //Twentyの数だけ20junksを出す

			junk_pop = GameObject.Instantiate 
				(Two_Junks, transform.position, transform.rotation)as GameObject;

			junk_pop.GetComponent<Release_Junk> ().Child_Color_Change
			(GetComponent<MeshRenderer>().material.color); //（残るオブジェクトを自分の色に変える処理を実行させる）処理を実行

		}

		for (int i = 0; i < Ten; i++) { //Twentyの数だけ10junksを出す
			
			junk_pop = GameObject.Instantiate 
			(Ten_Junks, transform.position, transform.rotation)as GameObject;

			junk_pop.GetComponent<Release_Junk> ().Child_Color_Change
			(GetComponent<MeshRenderer>().material.color); //（残るオブジェクトを自分の色に変える処理を実行させる）処理を実行

		}

		for (int i = 0; i < one; i++) { //oneの数だけjunkを出す

			junk_pop = GameObject.Instantiate 
			(junk, transform.position, transform.rotation)as GameObject;

			junk_pop.GetComponent<Junks_Con>().Change_Color
				(GetComponent<MeshRenderer>().material.color);

		}

		Twenty = 0; //Twentyの初期化

		Ten = 0; //Tenの初期化

		AS.clip = Hit_sound;//オーディオソースに破壊された音を付ける

		AS.Play (); //オーディオソースを再生する

		yield return new WaitForSeconds (1f); //1秒待つ


	Destroy (gameObject); //自分を破壊する

	}


	public void Shoot(Vector3 dir){//投げる処理

		rb.AddForce (dir * throw_power);

		CountStart = true;
		
	}


}
