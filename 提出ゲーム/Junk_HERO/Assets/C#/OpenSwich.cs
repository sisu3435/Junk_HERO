using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//攻撃することで特定のオブジェクトごと破壊されるスクリプト
public class OpenSwich : MonoBehaviour {

	public GameObject Open_Wall; // 破壊するオブジェクト


	void Start () {
		
	}


	void Update () {


	}


	void OnCollisionEnter(Collision cln){

		//攻撃を受けたら自分と破壊するオブジェクトを同時に破壊する
		if (cln.collider.tag == "Pl_Atk") {

			Destroy (Open_Wall);

			Destroy (gameObject);
		
		}
	
	}

}