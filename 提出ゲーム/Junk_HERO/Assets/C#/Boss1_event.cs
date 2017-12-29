using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_event : MonoBehaviour {//ボスが出現するスクリプト

	public GameObject Boss_Enemy; //ボスのGameObject

	public GameObject pop_position; //出現する位置


	void Start () {
		
	}
	

	void Update () {

		if (GetComponent<Boss_Event> ().boss_battle == true) { //ボスとの戦闘が開始されたら
			
			Boss_Enemy.gameObject.SetActive (true); //Bossを見えるようにする

			Destroy (gameObject); //自分を破壊する
		
		}
		
	}


}