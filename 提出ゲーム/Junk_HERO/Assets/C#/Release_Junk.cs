using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//10Junks、20Junksが出現した時子オブジェクトを残して自分を破壊する処理
public class Release_Junk : MonoBehaviour {

	float count = 3;



	void Start () {

	}
	

	void Update () {
		
		count -= Time.deltaTime;

		if (count <= 0) {

			transform.DetachChildren (); //親子関係を解除

			Destroy (gameObject); //自分を破壊する

		}

	}


	public void Child_Color_Change(Vector4 clr){
		
		for (int i = 0; i < transform.childCount; i++) { //子オブジェクトを切り離す処理

			Transform CT = transform.GetChild (i); //子オブジェクトを取得

			CT.GetComponent<Junks_Con> ().Change_Color (clr);

		}

	}

}