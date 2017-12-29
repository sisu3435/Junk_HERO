using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ArmerBossの動きを制御するスクリプト
public class ArmerBoss_Move : MonoBehaviour {
	
	Rigidbody rb; //Rigidbody

	public GameObject Player; //プレイヤーの位置を取得するための変数

	public bool Can_Move = true; //今動けるか

	float Timer = 5; //攻撃する間隔

	public GameObject Attack_Wave; //攻撃

	public GameObject Parent; //自分の親オブジェクト

	public GameObject Weak; //弱点

	AudioSource AS; //オーディオソース

	public AudioClip inpact_sound; //攻撃を受けた時の音

	public AudioClip jump_sound; //ジャンプ音

	public AudioClip attack_sound; //攻撃時の音

	public bool armer_broken = false; //鎧が壊されているか


	void Start () {
		
		AS = GetComponent<AudioSource> (); //ArmerBossのオーディオソースを取得する

		rb = Parent.GetComponent<Rigidbody> (); //ArmerBossのRigidbodyを取得する

		StartCoroutine ("Attack");//攻撃する

	}

	
	void Update () {
		
		if (Weak == null) { //弱点が破壊された時
			
			Weak = gameObject; //弱点を自分に設定

			armer_broken = true;

			Timer = 5; //攻撃までの時間を5秒にする

		}

		if (Can_Move) { //自分が動ける時

			Timer -= Time.deltaTime; //攻撃までの時間を減らす

			//向くべき方向のy軸の回転を取得
			Quaternion R_rotation = 
				Quaternion.LookRotation (Player.transform.position - transform.position);

			//向くべき方向を取得
			Quaternion go_rotation = new Quaternion (
				                        transform.rotation.x, R_rotation.y, transform.rotation.z, R_rotation.w);

			//親の向きを徐々に敵の方向へ向ける
			Parent.transform.rotation = Quaternion.Slerp 
			(Parent.transform.rotation, go_rotation, Time.deltaTime * 0.3f);

			if (Timer < 0) { //攻撃までの時間が0秒になった時攻撃して、攻撃までの時間を元に戻す
				
				StartCoroutine ("Attack");

				Timer = 5;

			}
		
		}
	
	}


	public IEnumerator Stop(){ //一定時間（2秒）停止する処理
		
		AS.clip = inpact_sound; //オーディオソースに攻撃を受けた時の音を付ける

		AS.Play (); //オーディオソースを再生する

		Can_Move = false;

		yield return new WaitForSeconds (2f);

		Can_Move = true;
	
	}


	IEnumerator Attack(){//攻撃する処理

		Can_Move = false; //動けなくなる

		AS.clip = jump_sound; //オーディオソースにジャンプする時の音を付ける

		AS.Play (); //オーディオソースを再生する

		rb.AddForce (transform.up * 5000); //跳躍する

		yield return new WaitForSeconds (0.9f); //0.9秒待つ

		Vector3 wave = new Vector3 //AttackWaveを置く位置を設定
			(
				transform.position.x,
				0.25f,
			    transform.position.z
			);

		Vector3 wave_size;

		//鎧が壊されていないなら大きな、破壊されているなら小さな攻撃にする
		if (!armer_broken) { 

			wave_size = new Vector3 (9, 1.5f, 9);

		} else {

			wave_size = new Vector3 (3, 1.5f, 3);

		}

		GameObject Wave = 
			Instantiate (Attack_Wave, wave, transform.rotation)as GameObject;//Attack_Waveをインスタンティートする

		//攻撃のサイズを調整する
		Wave.transform.localScale = wave_size;

		AS.clip = attack_sound; //オーディオソースに攻撃する時の音を付ける

		AS.Play (); //オーディオソースを再生する

		Can_Move = true; //動けるようになる

	}


}