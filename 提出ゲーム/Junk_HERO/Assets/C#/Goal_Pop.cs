using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Pop : MonoBehaviour { //ゴールを出現させるプログラム

	public GameObject Boss; //ボス


	void Start () {

		if (transform.childCount > 0) { //子オブジェクトを全て非実体化

			for (int i = 0; i < transform.childCount; i++) {

				GameObject GO = transform.GetChild (i).gameObject;

				GO.SetActive (false);

			}

		}

	}
	

	void Update () {

		if (Boss == null) { //ボスが破壊されたら
		
			Boss = gameObject; //自分をBossにする(Bossをnullでなくするため)

			if (transform.childCount > 0) { //子オブジェクトをすべて実体化
			
				for (int i = 0; i < transform.childCount; i++) {
					
					GameObject GO = transform.GetChild (i).gameObject;

					GO.SetActive (true);
				
				}
					 
			}

		}
		
	}

}