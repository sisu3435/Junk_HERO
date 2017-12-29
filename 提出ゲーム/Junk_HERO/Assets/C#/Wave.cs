using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {//ArmerBossの放つWave攻撃の処理

	Vector3 Size; //このオブジェクトの大きさを入れる変数

	float x = 0; //xの値を代入する変数

	float z = 0; //zの値を代入する変数

	AudioSource AS; //オーディオソース

	public AudioClip spread_sound; //広がっていく音


	void Start () {
		AS = GetComponent<AudioSource> (); //オーディオソースの取得

		AS.clip = spread_sound; //オーディオソースに広がっていく音を付ける

		AS.Play (); //オーディオソースを再生する

	}
	

	void FixedUpdate(){

		x += Time.deltaTime * 0.5f; //１秒ずつxを0.5ずつ増やす

		z += Time.deltaTime * 0.5f; //1秒ずつzを0.5ずつ増やす

		Size = new Vector3 (transform.localScale.x + x, transform.localScale.y, transform.localScale.z + z); //大きさにx、zを反映させた値を代入する

		transform.localScale = Size; //代入した値を実際のオブジェクトに反映させる

		if ((x) > 10) { //10秒後に自分を破壊する

			Destroy (gameObject);

		}

	}

	void OnCollisionEnter(Collision cln){

		if (cln.collider.tag == "Player") {

			GetComponent<MeshCollider> ().enabled = false;


		}

	}


}