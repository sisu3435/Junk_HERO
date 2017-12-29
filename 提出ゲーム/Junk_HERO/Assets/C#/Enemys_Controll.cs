using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemys_Controll : MonoBehaviour { //敵の動きを制御するスクリプト

	public Transform pl; //プレイヤー

	Rigidbody rb; //リジッドボディ

	bool Looked = false; //プレイヤーを見つけたか

	int rad = 0; //ランダム数

	float x; //x方向の移動用数値

	float z; //y方向の移動用数値

	float MT = 0; //移動時間 2秒

	float speed = 2f; //移動速度

	public int HP; //体力

	public GameObject remain_object; //消えた後に残るオブジェクト

	Animator anim; //アニメーター


	void Start () {
	
		anim = GetComponent<Animator> (); //アニメーターを取得

		rb = GetComponent<Rigidbody> ();

		pl = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if (HP <= 0) { //キャラクターが死んだときの処理
			

			if (remain_object != null) { //自分を破壊する処理

				Vector4 color = GetComponent<MeshRenderer> ().material.color;
		
				GetComponent<Collider> ().enabled = false;

				Vector3 pop_position = new Vector3
					( transform.position.x,1,transform.position.z);
				
				GameObject remain_pop =Instantiate 
					(remain_object, pop_position, remain_object.transform.rotation)as GameObject;

				remain_pop.GetComponent<Release_Junk> ().Child_Color_Change
				(color); //（残るオブジェクトを自分の色に変える処理を実行させる）処理を実行

			}

			Destroy (gameObject);
		}

		if (Looked) { //プレイヤーを見つけたとき追いかける処理
			
			Vector3 player_Transform = new Vector3(pl.position.x,transform.position.y,pl.position.z);

			Vector3 direction = player_Transform - transform.position;

			transform.position += (direction.normalized * speed * Time.deltaTime);

			transform.LookAt (player_Transform);
			
		} else { //プレイヤーが見つかっていない時ランダムに移動する処理

			MT -= Time.deltaTime; //移動する時間

			rb.velocity = new Vector3 (x, -2, z); //移動処理

			if (MT < 0) { //時間が過ぎたら移動方向を決定する
			
				rad = Random.Range (1, 5); //ランダムで方向を決定

				z = 0; //初期化

				x = 0; //初期化

				switch (rad) { //移動方向を決定

				case 1: //+z方向
					
					z = 3;

					break;
					
				case 2: //-z方向
					
					z = -3;

					break;
					
				case 3: //+x方向
					
					x = 3;

					break;
					
				case 4: //-x方向
					
					x = -3;

					break;
					
				}

				MT = 2; //移動時間初期化

			}

		}

	}


	void OnTriggerEnter(Collider col){
		
		if (col.tag == "Player") { 
			
			//索敵範囲内にプレイヤーが入ったとき追いかけるための変数をOnにする
			Looked = true;

			//アニメーションを開始するフラグを建てる
			anim.SetBool ("Looked", true);
			
		}

	}


	void OnCollisionEnter(Collision cln){

		if (cln.collider.tag == "Pl_Atk") { //攻撃を受けたときの処理

			//ダメージを取得
			int damage = cln.gameObject.GetComponent<Wepon_States> ().ATK;

			//ダメージ処理に移行
			Damage (damage);
			
		}

	}


	void Damage(int damage){ //ダメージ分HPを減らす処理
		
		HP -= damage;

	}

}