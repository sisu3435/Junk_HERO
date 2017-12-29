using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junks_Lighting : MonoBehaviour {//Junksを光らせるか判断するスクリプト

	//光らせるフラグ
	public bool Lighting = false;


	void Start () {
		
	}
	

	void Update () {

		//L入力でlightingを切り替える
		if (Input.GetKeyDown (KeyCode.L)) {

			if (Lighting) {
			
				Lighting = false;

			} else if (!Lighting) {

				Lighting = true;

			}
		
		}
	
	}

}