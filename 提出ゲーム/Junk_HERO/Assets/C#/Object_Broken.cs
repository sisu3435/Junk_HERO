using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Broken : MonoBehaviour {

	public GameObject remain_object;//破壊された時に残るオブジェクト

	public AudioSource AS; //オーディオソース

	public AudioClip break_sound; //破壊された時の音

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision cln){

		if (cln.collider.tag == "Pl_Atk"
		    || cln.collider.tag == "Break_Brock") {//プレイヤーの技2、技3を受けたとき

			StartCoroutine ("broken"); //自分を破壊する処理を実行する
		}
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
		(GetComponent<MeshRenderer>().material.color);

		yield return new WaitForSeconds (0.5f); //0.5秒待つ


		Destroy (gameObject); // 自分を破壊する

	}


}
