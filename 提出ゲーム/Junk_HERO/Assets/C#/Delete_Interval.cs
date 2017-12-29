using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Interval : MonoBehaviour { //時間、あるいは破壊されて自壊するスクリプト

	public float Delete_Count; //消えるまでの秒数

	public bool Start_Count = false; //カウントを始めるか

	public bool Deleted = false; //消すかどうか

	public GameObject remain_object; //消えた後に残るオブジェクト

	AudioSource AS; //オーディオソース

	public AudioClip break_sound; //破壊された時の音


	void Start () {

		AS = GetComponent<AudioSource> ();
		
	}

	

	void Update () {
		
		if (Start_Count) { //カウントを始める

			Delete_Count -= Time.deltaTime;
		
		}

		 //下へ行き過ぎた、またはカウントが０になった、または壊れた時、
		if (transform.position.y < -10 || Delete_Count <= 0) {
			
			StartCoroutine ("broken");

		}
		
	}


	void OnCollisionEnter(Collision cln){
		
		if (cln.collider.tag == "Pl_Atk") { //プレイヤーの攻撃を受けたとき

			StartCoroutine ("broken");
		
		}
	
	}


	void OnTriggerEnter(Collider col){
		
		if (col.tag == "Break_Brock") {
			
			//プレイヤーの一定範囲のオブジェクトを破壊する処理の範囲に入ったとき
			StartCoroutine ("broken");

		}
	
	}


	IEnumerator broken(){ //破壊されたときの処理

		GetComponent<BoxCollider> ().enabled = false; //オブジェクトの判定を外す

		GetComponent<MeshRenderer> ().enabled = false; //見えなくする

		AS.clip = break_sound; //オーディオソースに破壊された時の音を付ける

		AS.Play (); //オーディオソースを再生する

		//インスタンティートするオブジェクトの位置を決定する
		Vector3 rp_transform = new Vector3 (

			transform.position.x,
			transform.position.y + 1f,
			transform.position.z

		); 

		//残るオブジェクトをインスタンティートする
		GameObject remain_pop = 
			GameObject.Instantiate (remain_object,rp_transform,transform.rotation)
			as GameObject; 


		//（残るオブジェクトを自分の色に変える処理を実行させる）処理を実行
		remain_pop.GetComponent<Release_Junk> ().Child_Color_Change
		(GetComponent<MeshRenderer>().material.color); 

		yield return new WaitForSeconds (0.5f); //0.5秒待つ

		Destroy (gameObject); // 自分を破壊する
	}


}