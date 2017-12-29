using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Relase : MonoBehaviour //すべてのjunkを吐き出すオブジェクトの動作のスクリプト
{

	GameObject player; //プレイヤー

	public GameObject Junk; //junk

	public GameObject Ten; //Junkを10個同時に出す

	public GameObject Twenty; //  〃  20

	public AudioSource AS; //オーディオソース

	public AudioClip pop_sound; //junkを出す音


	void Start () {
		
		player = GameObject.Find ("Junk_Robot")as GameObject; //プレイヤーの取得

		StartCoroutine ("All_Release"); //プレイヤーが持つ全てのジャンクを出す処理を実行する


	}
	

	void Update () {
		
	}


	private IEnumerator All_Release(){ //すべてのjunkを吐き出す処理
		
		player.GetComponent<SphereCollider> ().enabled = false; //プレイヤーがjunkを拾えないようにする

		int total = player.GetComponent<Player_Attack> ().Total_Junks; //プレイヤーのジャンクの総数をtotalに記録する

		player.GetComponent<Player_Attack> ().Total_Junks = 0; //プレイヤーの持っているjunkの数を0にする

		for (; total > 0;) { //totalが0になるまでjunkを1ずつ吐き出す

			GameObject Junkpop; // junkを入れるGameObject



			total -= 1; //totalを1ずつ減らす

			Junkpop = GameObject.Instantiate (Junk)as GameObject; //junkをインスタンティートする

			float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);//向きを取得する

			Vector3 dir = new Vector3 (Mathf.Sin (angleDir), 0, Mathf.Cos (angleDir)); //junkを飛ばすベクトルの向きを決める


			Junkpop.transform.position = new Vector3 ( //junkの位置を決定する
				transform.position.x + Mathf.Sin (angleDir) + 1f,
				transform.position.y + 0.4f,
				transform.position.z + Mathf.Cos (angleDir) + 1f);

			Rigidbody jrb = Junkpop.GetComponent <Rigidbody> (); //junkのRigidBodyを取得する

			jrb.AddForce (dir * 50); //前にjunkを飛ばす

			AS.clip = pop_sound; //オーディオソースにjunkを出す音を付ける

			AS.Play (); //オーディオソースを再生する

			yield return new WaitForSeconds (0.01f); //0.01秒待つ

		}

		player.GetComponent<SphereCollider> ().enabled = true; //プレイヤーがjunkを回収できるようにする

	}


}