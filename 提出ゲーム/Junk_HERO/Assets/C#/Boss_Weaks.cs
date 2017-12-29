using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Boss_Weaks : MonoBehaviour { //Bossの弱点のスクリプト

	public GameObject Weaks; //弱点

	public Material material1; //通常のマテリアル

	public Material material2; //ダメージを受けた時のマテリアル

	public int HP; //弱点のHP

	public AudioSource AS; //オーディオソース

	public AudioClip damaged; //ダメージを受けた時の音

	void Start () {
		
	}


	void Update () {
		
		if (HP <= 0) { //HPが0になった時
			
			StartCoroutine ("destroyed"); //破壊された時の処理を実行する

			HP = 999;
		}

	}


	void OnTriggerEnter(Collider col){

		if (col.tag == "Pl_Atk") {//攻撃を受けた時の処理

			StartCoroutine("Damaged"); //ダメージを受けた時の処理を実行する

			HP -=  col.gameObject.GetComponent<Wepon_States> ().ATK; //HPを攻撃力分減らす


		}


	}



	IEnumerator Damaged(){ //ダメージを受けた時の処理
		
		Weaks.GetComponent<MeshRenderer> ().material = material2; //ダメージを受けた時のマテリアルにする

		yield return new WaitForSeconds (0.05f); //0.05秒待つ

		Weaks.GetComponent<MeshRenderer> ().material = material1; //元に戻す

		yield return new WaitForSeconds (0.05f); //0.05秒待つ


	}


	IEnumerator destroyed(){ //破壊された時の処理

		GetComponent<MeshCollider> ().isTrigger = false;
		
		for (int i = 0; i < Weaks.transform.childCount; i++) { //子オブジェクトを切り離す処理

			Transform CT = Weaks.transform.GetChild(i); //子オブジェクトを取得

			CT.gameObject.layer = LayerMask.NameToLayer("Break_Object"); //子オブジェクトのレイヤーを変更

			CT.GetComponent<MeshCollider> ().isTrigger = false; //判定を真にする

			Rigidbody CR = CT.GetComponent<Rigidbody> (); //子オブジェクトのRigidbodyを取得する

			if (CR != null) { //Rigidbodyがnullでない時

				CR.useGravity = true; //重力をかける

				CR.constraints = RigidbodyConstraints.None; //子オブジェクトのロックを外す

				CR.AddForce (CT.transform.up * 1000
					+ CT.transform.forward * 100); // 斜め前に力を加える

			}

		}

		Weaks.transform.DetachChildren (); //子オブジェクトとの親子関係を解除する

		yield return new WaitForSeconds (0.5f);

		Destroy (Weaks); //弱点を破壊する

		Destroy (gameObject); //自分を破壊する

	}


}
