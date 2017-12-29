using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_HP : MonoBehaviour { //ボスのHPを管理するスクリプト
	
	int HP = 100; //ボスの体力

	public GameObject Parent; //ボスの親オブジェクト

	public Material material1; //通常のマテリアル

	public Material material2; //ダメージを受けた時のマテリアル

	public int Boss_Drop_Count; //ボスが落とすオブジェクトの数

	public GameObject Drop_Object; //ボスが落とすオブジェクト

	public bool death = false; //死んだかの判定

	public AudioSource AS; //オーディオソース

	public AudioClip damaged; //ダメージを受けた時の音

	public AudioClip Destroy_sound; //破壊された時の音

	Vector4 color;


	void Start () {

		color = GetComponent<MeshRenderer> ().material.color;
		
	}
	

	void Update () {
		
		if (HP <= 0 && !death) { //HPが0になって死んでいる判定が出ていない時に死んだ判定を出す
			
			destroyed (); //死んだ時の処理を実行

			death = true; //死んだ判定

		}

		
	}
	void OnTriggerEnter(Collider col){
		
		if (col.tag == "Pl_Atk") { //プレイヤーの攻撃を受けた時
			
			HP -= col.GetComponent<Wepon_States> ().ATK; //HPを攻撃力文減らす

			StartCoroutine("Damaged"); //ダメージを受けた時の処理

		}

	}


	IEnumerator Damaged(){
		
		AS.clip = damaged; //オーディオソースにダメージを受けた時の音を付ける

		AS.Play (); //オーディオソースを再生する

		GetComponent<MeshRenderer> ().material = material2; //ダメージを受けた時のマテリアルにする

		yield return new WaitForSeconds (0.05f); //0.05秒待つ

		GetComponent<MeshRenderer> ().material = material1; //元のマテリアルに戻す

		yield return new WaitForSeconds (0.05f); //0.05秒待つ

	}


	public void destroyed(){ //破壊されたときの処理


		Parent.GetComponent<SphereCollider> ().enabled = false; //見た目を消す

		GetComponent<SphereCollider> ().enabled = false; //コライダーを外す

		AS.clip = Destroy_sound; //オーディオソースに破壊された時の音を付ける

		AS.PlayOneShot (AS.clip); //オーディオソースを再生する

		for (int i = 0;i < Boss_Drop_Count; i++){ //ボスが落とす数だけボスが落とす物を出す

			GameObject remain_pop = Instantiate (Drop_Object, transform.position, transform.rotation)as GameObject;

			remain_pop.GetComponent<Release_Junk> ().Child_Color_Change
			(color); //（残るオブジェクトを自分の色に変える処理を実行させる）処理を実行


		}

		Destroy (Parent); //Bossを破壊する


	}
}
