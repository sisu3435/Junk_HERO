using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour {//攻撃を受けて止まるスクリプト

	public GameObject Body;//ArmerBossを動かしているBody





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10) {//y座標が-10より下になったら自分を破壊する
			Destroy (gameObject);
		}
		
	}
	void OnCollisionEnter(Collision cln){
		
		if (cln.collider.tag == "Pl_Atk") {//プレイヤーからの攻撃を受けた時、自分の動きを止める処理を実行する
			Body.GetComponent<ArmerBoss_Move>().StartCoroutine ("Stop");
		}
	}
}
